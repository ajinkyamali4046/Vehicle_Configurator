import React from "react";

function SignUpForm() {
    const [state, setState] = React.useState({
        name: "",
        email: "",
        password: "",
        username: "",
        address_line1: "",
        address_line2: "",
        city: "",
        company_name: "",
        gst_number: "",
        pin_code: "",
        state: "",
        telephone: ""
    });

    const handleChange = evt => {
        const value = evt.target.value;
        setState({
            ...state,
            [evt.target.name]: value
        });
    };

    const handleOnSubmit = evt => {
        evt.preventDefault();
    
        console.log("Form data to submit:", state); // Log the form data
    
        fetch('http://localhost:5254/api/User/register', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(state)
        })
        .then(response => response.json())
        .then(data => {
            console.log("Response data:", data); // Log the response data
            if (data.errors) {
                alert("Registration failed: " + Object.values(data.errors).flat().join(', '));
            } else {
                alert("SIGN UP SUCCESSFUL, Now Login");
                setState({
                    addressLine1: "",
                    addressLine2: "",
                    city: "",
                    companyName: "",
                    email: "",
                    gstNumber: "",
                    password: "",
                    pinCode: "",
                    state: "",
                    telephone: "",
                    username: ""
                });
            }
        })
        .catch(error => {
            console.error('Registration failed:', error);
            alert("Registration failed. Please try again.");
        });
    };
    

    

    return (
        <div className="form-container sign-up-container">
            <form onSubmit={handleOnSubmit}>
                <h1>Create Account</h1>
                <span style={{ padding: "7px" }}>Unlock Your Vehicle Experience —Register Now</span>
                <input
                    type="text"
                    name="name"
                    value={state.name}
                    onChange={handleChange}
                    placeholder="Name"
                    required
                />
                <input
                    type="email"
                    name="email"
                    value={state.email}
                    onChange={handleChange}
                    placeholder="Email"
                    required
                />
                <input
                    type="text"
                    name="username"
                    value={state.username}
                    onChange={handleChange}
                    placeholder="Username"
                    required
                />
                <input
                    type="password"
                    name="password"
                    value={state.password}
                    onChange={handleChange}
                    placeholder="Password"
                    required
                />
                <input
                    type="text"
                    name="addressLine1"
                    value={state.addressLine1}
                    onChange={handleChange}
                    placeholder="Address Line 1"
                    required
                />
                <input
                    type="text"
                    name="address_line2"
                    value={state.address_line2}
                    onChange={handleChange}
                    placeholder="Address Line 2"
                />
                <input
                    type="text"
                    name="city"
                    value={state.city}
                    onChange={handleChange}
                    placeholder="City"
                    required
                />
                <input
                    type="text"
                    name="companyName"
                    value={state.companyName}
                    onChange={handleChange}
                    placeholder="Company Name"
                    required
                />
                <input
                    type="text"
                    name="gstNumber"
                    value={state.gstNumber}
                    onChange={handleChange}
                    placeholder="GST Number"
                    required
                />
                <input
                    type="text"
                    name="pinCode"
                    value={state.pinCode}
                    onChange={handleChange}
                    placeholder="Pin Code"
                    required
                />
                <input
                    type="text"
                    name="state"
                    value={state.state}
                    onChange={handleChange}
                    placeholder="State"
                />
                <input
                    type="text"
                    name="telephone"
                    value={state.telephone}
                    onChange={handleChange}
                    placeholder="Telephone"
                />
                <button className="sign-up-btn">Sign Up</button>
            </form>
        </div>
    );
}

export default SignUpForm;
