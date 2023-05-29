import React, { useEffect, useState } from "react";
import axios from "axios";
import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";

import PermissionsList from "./pages/permissions/PermissionsList";
import PermissionTypesList from "./pages/permissionTypes/PermissionTypesList";

function App() {
  const [permissions, setPermissions] = useState([]);
  const [permissionTypes, setPermissionTypes] = useState([]);

  const getInitialData = async () => {
    const permissionsResult = await axios.get(
      `${process.env.REACT_APP_PERMISSIONS_API_URL}/Permissions`
    );
    const permissionTypesResult = await axios.get(
      `${process.env.REACT_APP_PERMISSIONS_API_URL}/PermissionTypes`
    );

    setPermissions(permissionsResult.data);
    setPermissionTypes(permissionTypesResult.data);
  };

  useEffect(() => {
    getInitialData();
  }, []);

  return (
    <BrowserRouter>
      <Routes>
        <Route
          path="/permissions"
          element={<PermissionsList permissions={permissions} />}
        />
        <Route
          path="/permissionTypes"
          element={<PermissionTypesList permissionTypes={permissionTypes} />}
        />
        <Route path="*" element={<Navigate to="/permissions" replace />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
