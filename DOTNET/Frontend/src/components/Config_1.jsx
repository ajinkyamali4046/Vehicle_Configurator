import React, { useState, useEffect, useContext } from "react";
import { useNavigate } from 'react-router-dom';
import { ResetContext } from "../contexts/ResetContext";
import "../styles/configurator.css";

export const Config_1 = (props) => {
  const [segments, setSegments] = useState([]);
  const [selectedSegment, setSelectedSegment] = useState("");
  const [selectedManufacturer, setSelectedManufacturer] = useState("");
  const [manufacturers, setManufacturers] = useState([]);
  const [models, setModels] = useState([]);
  const [selectedModel, setSelectedModel] = useState("");
  const [components, setComponents] = useState([]);
  const [selectedModelDetails, setSelectedModelDetails] = useState({});
  const [exteriorComponents, setExteriorComponents] = useState([]);
  const [interiorComponents, setInteriorComponents] = useState([]);
  const [coreComponents, setCoreComponents] = useState([]);
  const [orderSize, setOrderSize] = useState(1);
  const [showModifiableComponents, setShowModifiableComponents] = useState(false);
  const { segmentSelectedTop, setSegmentSelectedTop } = useContext(ResetContext);
  const [isStandardExpanded, setIsStandardExpanded] = useState(false);
  const [isExteriorExpanded, setIsExteriorExpanded] = useState(false);
  const [isInteriorExpanded, setIsInteriorExpanded] = useState(false);
  const [isCoreExpanded, setIsCoreExpanded] = useState(false);

  useEffect(() => {
    const storedOrderSize = sessionStorage.getItem("orderSize");
    if (storedOrderSize) {
      setOrderSize(parseInt(storedOrderSize));
    }
  }, []);

  useEffect(() => {
    sessionStorage.setItem("orderSize", orderSize);
  }, [orderSize]);

  useEffect(() => {
    setSelectedSegment("");
    setComponents([]);
    setCoreComponents([]);
    setSelectedModel("");
    setSelectedManufacturer("");
    setInteriorComponents([]);
    setExteriorComponents([]);
    setModels([]);
    setManufacturers([]);
    setSelectedModelDetails({});
    setOrderSize(1);
    setShowModifiableComponents(false);
  }, [segmentSelectedTop]);

  useEffect(() => {
    fetchSegments();
  }, []);

  useEffect(() => {
    if (selectedModel) {
      fetchComponents(selectedModel);
      fetchExteriorComponents(selectedModel);
      fetchInteriorComponents(selectedModel);
      fetchCoreComponents(selectedModel);
      fetchModelDetails(selectedModel);
    }
  }, [selectedModel]);

  const fetchSegments = () => {
    const token = sessionStorage.getItem("jwtToken");

    fetch("http://localhost:5254/api/Segment", {
      method: "GET",
      // headers: {
      //   "Authorization": `Bearer ${token}`,
      //   "Content-Type": "application/json",
      // },
    })
      .then(response => {
        if (!response.ok) {
          throw new Error(`HTTP error! status: ${response.status}`);
        }
        return response.json();
        
      })
      .then(data => setSegments(data) )
      .catch(error => console.error("Error fetching segments:", error));
  };

  const fetchManufacturers = (segmentId) => {
    const token = sessionStorage.getItem("jwtToken");

    fetch(`http://localhost:5254/api/Manufacturer/${segmentId}`, {
      method: "GET",
      // headers: {
      //   "Authorization": `Bearer ${token}`,
      //   "Content-Type": "application/json",
      // },
    })
      .then(response => {
        if (!response.ok) {
          throw new Error(`HTTP error! status: ${response.status}`);
        }
        const dt= response.json();
        console.log(dt);
        return dt;
      })
      .then(data => setManufacturers(data))
      .catch(error => console.error("Error fetching manufacturers:", error));
  };

  const fetchModels = (segmentId, manufacturerId) => {
    const token = sessionStorage.getItem("jwtToken");

    fetch(`http://localhost:5254/api/Model/${segmentId}/${manufacturerId}`, {
      method: "GET",
      // headers: {
      //   "Authorization": `Bearer ${token}`,
      //   "Content-Type": "application/json",
      // },
    })
      .then(response => {
        if (!response.ok) {
          throw new Error(`HTTP error! status: ${response.status}`);
        }
        return response.json();
      })
      .then(data => {
        setModels(data);
        localStorage.setItem('model', JSON.stringify(data));
      })
      .catch(error => console.error("Error fetching models:", error));
  };

  const fetchModelDetails = (modelId) => {
    const token = sessionStorage.getItem("jwtToken");

    fetch(`http://localhost:5254/api/Model/${modelId}`, {
      method: "GET",
      // headers: {
      //   "Authorization": `Bearer ${token}`,
      //   "Content-Type": "application/json",
      // },
    })
      .then(response => {
        if (!response.ok) {
          throw new Error(`HTTP error! status: ${response.status}`);
        }
        return response.json();
      })
      .then(data => {
        setSelectedModelDetails(data);
        sessionStorage.setItem('selectedModelDetails', JSON.stringify(data));
        setOrderSize(data.minQty);
      })
      .catch(error => console.error("Error fetching model details:", error));
  };

  const fetchComponents = (modelId) => {
    const token = sessionStorage.getItem("jwtToken");

    fetch(`http://localhost:5254/api/Vehicle/S/${modelId}`, {
      method: "GET",
      // headers: {
      //   "Authorization": `Bearer ${token}`,
      //   "Content-Type": "application/json",
      // },
    })
      .then(response => {
        if (!response.ok) {
          throw new Error(`HTTP error! status: ${response.status}`);
        }
        return response.json();
      })
      .then(data => setComponents(data))
      .catch(error => console.error("Error fetching components:", error));
  };

  const fetchExteriorComponents = (modelId) => {
    const token = sessionStorage.getItem("jwtToken");

    fetch(`http://localhost:5254/api/Vehicle/E/${modelId}`, {
      method: "GET",
      // headers: {
      //   "Authorization": `Bearer ${token}`,
      //   "Content-Type": "application/json",
      // },
    })
      .then(response => {
        if (!response.ok) {
          throw new Error(`HTTP error! status: ${response.status}`);
        }
        return response.json();
      })
      .then(data => setExteriorComponents(data))
      .catch(error => console.error("Error fetching exterior components:", error));
  };

  const fetchInteriorComponents = (modelId) => {
    const token = sessionStorage.getItem("jwtToken");

    fetch(`http://localhost:5254/api/Vehicle/I/${modelId}`, {
      method: "GET",
      // headers: {
      //   "Authorization": `Bearer ${token}`,
      //   "Content-Type": "application/json",
      // },
    })
      .then(response => {
        if (!response.ok) {
          throw new Error(`HTTP error! status: ${response.status}`);
        }
        return response.json();
      })
      .then(data => setInteriorComponents(data))
      .catch(error => console.error("Error fetching interior components:", error));
  };

  const fetchCoreComponents = (modelId) => {
    const token = sessionStorage.getItem("jwtToken");

    fetch(`http://localhost:5254/api/Vehicle/C/${modelId}`, {
      method: "GET",
      // headers: {
      //   "Authorization": `Bearer ${token}`,
      //   "Content-Type": "application/json",
      // },
    })
      .then(response => {
        if (!response.ok) {
          throw new Error(`HTTP error! status: ${response.status}`);
        }
        return response.json();
      })
      .then(data => setCoreComponents(data))
      .catch(error => console.error("Error fetching core components:", error));
  };

  const handleSegmentChange = (event) => {
    const segmentId = event.target.value;
    setSelectedSegment(segmentId);
    setSelectedManufacturer("");
    setManufacturers([]);
    setModels([]);
    fetchManufacturers(segmentId);
  };

  const handleManufacturerChange = (event) => {
    const manufacturerId = event.target.value;
    setSelectedManufacturer(manufacturerId);
    setModels([]);
    fetchModels(selectedSegment, manufacturerId);
  };

  const handleModelChange = (event) => {
    const selectedModelId = event.target.value;
    setSelectedModel(selectedModelId);
    fetchModelDetails(selectedModelId);
    fetchComponents(selectedModelId);
  };

  const incrementOrderSize = () => {
    setOrderSize(prevSize => prevSize + 1);
  };

  const decrementOrderSize = () => {
    if (orderSize > selectedModelDetails.minQty) {
      setOrderSize(prevSize => prevSize - 1);
    }
  };

  const navigate = useNavigate();

  const handleDefaultClick = () => {
    
    navigate('/InvoiceGenerator', { state: { selectedId: selectedModelDetails.id, orderSize: orderSize } });
  };

  const handleConfigureClick = () => {
    console.log("Navigating to AlternateModifier with ID:", selectedModelDetails.id);
    navigate('/AlternateModifier', {
      state: {
        selectedId: selectedModelDetails.id,
        orderSize: orderSize
      }
    });
  };

  const handleModifyClick = () => {
    setSelectedModel("");
  };





  const toggleSection = (section) => {
    switch(section) {
      case 'standard':
        setIsStandardExpanded(!isStandardExpanded);
        break;
      case 'exterior':
        setIsExteriorExpanded(!isExteriorExpanded);
        break;
      case 'interior':
        setIsInteriorExpanded(!isInteriorExpanded);
        break;
      case 'core':
        setIsCoreExpanded(!isCoreExpanded);
        break;
      default:
        break;
    }
  };
  

  return (
    <div id="configurator" className="text-center">
      <div className="container">
        <div className="section-title">
          <h2>Configurations</h2>
        </div>
        <div className="row justify-content-center text-center">
          <div className="col-md-3 mb-3">
            <h3>Segment</h3>
            <select
              className="custom-select"
              value={selectedSegment ?? ""}
              onChange={handleSegmentChange}
            >
              <option value="">Select Segment</option>
              {segments.map((segment) => (
                <option key={segment.id} value={segment.id}>
                  {segment.name}
                </option>
              ))}
            </select>
          </div>
          <div className={`col-md-3 mb-3 ${selectedSegment ? "" : "disabled"}`}>
            <h3>Manufacturer</h3>
            <select
              className="custom-select"
              value={selectedManufacturer}
              onChange={handleManufacturerChange}
              disabled={!selectedSegment}
            >
              <option value="">Select Manufacturer</option>
              {manufacturers.map((manufacturer) => (
                <option key={manufacturer.id} value={manufacturer.id}>
                  {manufacturer.name}
                </option>
              ))}
            </select>
          </div>
          <div
            className={`col-md-3 mb-3 ${selectedManufacturer ? "" : "disabled"}`}
          >
            <h3>Model</h3>
            <select
              className="custom-select"
              value={selectedModel}
              onChange={handleModelChange}
              disabled={!selectedManufacturer}
            >
              <option value="">Select Model</option>
              {Array.isArray(models) && models.map((model) => (
                <option key={model.id} value={model.id}>
                  {model.modName}
                </option>
              ))}
            </select>
          </div>
        </div>

        {selectedModel && (
          <div>
            <div className="col-md-4 model-details-container" style={{ width: "500px", marginLeft: "30%" }}>
              <div className="card card-container model-details-wrapper" style={{ marginBottom: "10px" }}>
                <div className="card-body model-details" >
                  <h3>Selected Model</h3>
                  <img src={selectedModelDetails.imagePath} alt="Model Image" />
                  <div className="seldata">
                    <p>Base Price: {selectedModelDetails.price}</p>
                    <p>
                      Min. Quantity: {selectedModelDetails.minQty}
                      <br /> <br />
                      <span className="ml-1 mr-1" style={{ marginTop: "10px" }}>Order Qty: {orderSize}</span> <br /><br />
                      <a className="btn btn-sm btn-primary" onClick={decrementOrderSize} style={{ marginLeft: "15px", cursor: "pointer", fontSize:"25px" }}>➖</a>
                      <a className="btn btn-sm btn-primary ml-2" onClick={incrementOrderSize} style={{ marginLeft: "15px", cursor: "pointer", fontSize:"25px" }}>➕</a>
                    </p>
                  </div>
                </div>
              </div>
            </div>

            <div className="col-md-8">
              <h3>Default Configuration:</h3>
              <div className="components-grid" id="comp">
                {/* Standard Components */}
                <div className="card card-container">
                  <div className="card-body component-list">
                    <h4 onClick={() => toggleSection('standard')} style={{ cursor: "pointer" }}>
                      Standard Components {isStandardExpanded ? "🔼" : "🔽"}
                    </h4>
                    {isStandardExpanded && (
                      <ul>
                        {components.map((component) => (
                          <li key={component.comp_id}>
                            <strong>&#8226;</strong> {component.comp_name}
                          </li>
                        ))}
                      </ul>
                    )}
                  </div>
                </div>

                {/* Core Components */}
                <div className="card card-container">
                  <div className="card-body component-list">
                    <h4 onClick={() => toggleSection('core')} style={{ cursor: "pointer" }}>
                      Core Components {isCoreExpanded ? "🔼" : "🔽"}
                    </h4>
                    {isCoreExpanded && (
                      <ul>
                        {coreComponents.map((component) => (
                          <li key={component.comp_id}>
                            <strong>&#8226;</strong> {component.comp_name}
                          </li>
                        ))}
                      </ul>
                    )}
                  </div>
                </div>

                {/* Exterior Components */}
                <div className="card card-container">
                  <div className="card-body component-list">
                    <h4 onClick={() => toggleSection('exterior')} style={{ cursor: "pointer" }}>
                      Exterior Components {isExteriorExpanded ? "🔼" : "🔽"}
                    </h4>
                    {isExteriorExpanded && (
                      <ul>
                        {exteriorComponents.map((component) => (
                          <li key={component.comp_id}>
                            <strong>&#8226;</strong> {component.comp_name}
                          </li>
                        ))}
                      </ul>
                    )}
                  </div>
                </div>

                {/* Interior Components ▲ ▼ */}
                <div className="card card-container">
                  <div className="card-body component-list">
                    <h4 onClick={() => toggleSection('interior')} style={{ cursor: "pointer" }}>
                      Interior Components {isInteriorExpanded ? "🔼" : "🔽"}
                    </h4>
                    {isInteriorExpanded && (
                      <ul>
                        {interiorComponents.map((component) => (
                          <li key={component.comp_id}>
                            <strong>&#8226;</strong> {component.comp_name}
                          </li>
                        ))}
                      </ul>
                    )}
                  </div>
                </div>
              </div>

              {/* Model Buttons */}
              <div className="model-buttons">
                <button onClick={handleDefaultClick} style={{ backgroundColor: "dodgerblue", height: "35px", width: "100px", borderRadius: "10px", marginRight: "10px" }}>
                  Confirm
                </button>
                <button onClick={handleConfigureClick} style={{ backgroundColor: "dodgerblue", height: "35px", width: "100px", borderRadius: "10px", marginRight: "10px" }}>
                  Configure
                </button>
                <button onClick={handleModifyClick} style={{ backgroundColor: "dodgerblue", height: "35px", width: "100px", borderRadius: "10px" }}>
                  Cancel
                </button>
              </div>
            </div>
          </div>
        )}
      </div>
    </div>
  );
};

export default Config_1;
