import React, { useEffect, useState } from "react";
import axios from "axios";
import { BrowserRouter, Routes, Route } from "react-router-dom";

import PermissionTypesList from "./pages/permissionTypes/PermissionTypesList";

function App() {
  const [permissionTypes, setPermissionTypes] = useState([]);

  const getInitialData = async () => {
    const permissionTypesResult = await axios.get(
      `${process.env.REACT_APP_PERMISSIONS_API_URL}/PermissionTypes`
    );

    setPermissionTypes(permissionTypesResult.data);
  };

  useEffect(() => {
    getInitialData();
  }, []);

  return (
    <BrowserRouter>
      <Routes>
        <Route
          path="/permissionTypes"
          element={<PermissionTypesList permissionTypes={permissionTypes} />}
        />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
