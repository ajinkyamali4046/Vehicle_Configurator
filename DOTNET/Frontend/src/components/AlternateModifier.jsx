import React, { useState, useEffect } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import "../styles/AlternateModifier.css";

const AlternateModifier = () => {
  const [components, setComponents] = useState([]);
  const [selectedComponentIds, setSelectedComponentIds] = useState([]);
  const [alternateComponents, setAlternateComponents] = useState({});
  const [selectedAlternateComponentIds, setSelectedAlternateComponentIds] = useState({});
  const [totalDelta, setTotalDelta] = useState(0);

  const location = useLocation();
  const navigate = useNavigate();
  const { selectedId, orderSize } = location.state;

  const token = sessionStorage.getItem("jwtToken");

  // Fetch components when the component mounts
  useEffect(() => {
    const fetchComponents = async () => {
      try {
        const response = await fetch(`http://localhost:5254/api/Vehicle/config/${selectedId}/Y`);
        if (!response.ok) throw new Error("Failed to fetch components");

        const data = await response.json();
        setComponents(data);
      } catch (error) {
        console.error("Error fetching components:", error);
      }
    };

    if (selectedId) fetchComponents();
  }, [selectedId]);

  // Handle checkbox change
  const handleCheckboxChange = async (compId, checked) => {
    if (checked) {
      setSelectedComponentIds((prevIds) => [...prevIds, compId]);

      try {
        const response = await fetch(`http://localhost:5254/api/AlternateComponents/alternate-components/${selectedId}/${compId}`);
        if (!response.ok) throw new Error("Failed to fetch alternate components");

        sessionStorage.setItem("c_id",compId);
        const data = await response.json();
        setAlternateComponents((prevState) => ({ ...prevState, [compId]: data }));
        setSelectedAlternateComponentIds((prevState) => ({ ...prevState, [compId]: "" }));
      } catch (error) {
        console.error("Error fetching alternate components:", error);
      }
    } else {
      setSelectedComponentIds((prevIds) => prevIds.filter((id) => id !== compId));
      setAlternateComponents((prevState) => {
        const newState = { ...prevState };
        delete newState[compId];
        return newState;
      });
      setSelectedAlternateComponentIds((prevState) => {
        const newState = { ...prevState };
        delete newState[compId];
        return newState;
      });
    }
  };

  // Handle alternate component selection
  const handleAlternateChange = (compId, selectedValue) => {
    setSelectedAlternateComponentIds((prevState) => ({ ...prevState, [compId]: selectedValue }));
  };

  // Handle invoice generation
  // const handleInvoiceClick = async () => {
  //   try {
  //     const deltaPrices = await Promise.all(
  //       Object.values(selectedAlternateComponentIds).map(async (id) => {
  //         const response = await fetch(`http://localhost:5254/api/AlternateComponents/alt/${selectedId}/${id}`);
  //         if (!response.ok) throw new Error(`HTTP error! status: ${response.status}`);

  //         const data = await response.json();
  //         const deltaPrice = parseFloat(data.deltaPrice);
  //         return isNaN(deltaPrice) ? 0 : deltaPrice;
  //       })
  //     );

  //     const total = deltaPrices.reduce((a, b) => a + b, 0);
  //     setTotalDelta(total);
  //     navigateToInvoice(total);
  //   } catch (error) {
  //     console.log("Fetch failed", error);
  //   }
  // };

  const handleInvoiceClick = async () => {
    const fetchAndSumDeltaPrices = async () => {
    try {
      const deltaPrices = await Promise.all(
        Object.values(selectedAlternateComponentIds).map(async (id) => {
          console.log("In handle invoice click")
          const response = await fetch(`http://localhost:5254/api/AlternateComponents/alt/${selectedId}/${id}`);
          if (!response.ok) throw new Error(`HTTP error! status: ${response.status}`);
  
          const text = await response.text(); 
          // Read the response as text first
          if (!text) {
            console.warn(`Empty response for component ID: ${id}`);
            return 0; // If the response body is empty, return 0 as delta price
          }
  
          const data = JSON.parse(text);
          console.log("textual",data)
          const deltaPrice = parseFloat(data.deltaPrice);
          console.log(deltaPrice+"price"); // Manually parse the JSON from the text
          return isNaN(deltaPrice) ? 0 : deltaPrice;
        })
      );
  
      const total = deltaPrices.reduce((a, b) => a + b, 0);
      console.log(total+" totallllllll")
      setTotalDelta(total);
      navigateToInvoice(200);
    } catch (error) {
      console.log("Fetch failed", error);
    }
  };
  

  // Navigate to invoice page
  const navigateToInvoice = (totalDelta) => {
    navigate("/InvoiceGenerator", {
      state: {
        nonCheckedComponentIds: components
          .filter((component) => !selectedComponentIds.includes(component.id))
          .map((component) => component.id),
        selectedDropdownIds: Object.values(selectedComponentIds),
        selectedId:selectedId,
        orderSize:orderSize,
        totalDeltaa:totalDelta,
      },
    });
  };

  fetchAndSumDeltaPrices();
}

  

  // Handle modifying configurations
  const handleModifyClick = () => {
    navigate("/Configurations", {
      state: {
        selectedId,
        orderSize,
        selectedComponentIds,
        selectedAlternateComponentIds,
      },
    });
  };

  return (
    <>
      <center><h1>Components List</h1></center>
      <div className="data_disp">
        <ul>
          {components.map((component) => (
            <li key={component.id}>
              <div className="checkbox-wrapper-7" id="chec">
                <input
                  id={`checkbox-${component.id}`}
                  className="custom-checkbox"
                  type="checkbox"
                  checked={selectedComponentIds.includes(component.id)}
                  onChange={(e) => handleCheckboxChange(component.id, e.target.checked)}
                />
                <label className="tgl-btn" htmlFor={`checkbox-${component.id}`}></label>
              </div>
              <span className="compnamecheckbox" style={{ fontSize: "large" }}>{component.comp_name}</span>
              {selectedComponentIds.includes(component.id) && (
                <select
                  value={selectedAlternateComponentIds[component.id] || ""}
                  onChange={(e) => handleAlternateChange(component.id, e.target.value)}
                  disabled={!selectedComponentIds.includes(component.id)}
                  className="dropdwn"
                >
                  <option value="">Select Alternate Component</option>
                  {alternateComponents[component.id] &&
                    alternateComponents[component.id].map((altComp) => (
                      <option key={altComp.id} value={altComp.id}>
                        {altComp.comp_name} (Delta Price: {altComp.delta_price})
                      </option>
                    ))}
                </select>
              )}
            </li>
          ))}
        </ul>
      </div>
      <div className="two_btn">
        <button onClick={handleInvoiceClick}>Confirm</button>
        <button onClick={handleModifyClick} style={{ marginLeft: "20px" }}>Cancel</button>
      </div>
    </>
  );
};

export default AlternateModifier;
