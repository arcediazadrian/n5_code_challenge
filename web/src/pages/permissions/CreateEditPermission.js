import React, { useEffect, useState } from "react";

import { MenuItem, TextField, Button } from "@mui/material";
import { Box } from "@mui/system";
import DeleteIcon from "@mui/icons-material/Delete";
import Select from "@mui/material/Select";
import { FormControl, InputLabel, FormHelperText } from "@mui/material";
import Stack from "@mui/material/Stack";
import Snackbar from "@mui/material/Snackbar";
import Alert from "@mui/material/Alert";
import { useParams } from "react-router-dom";
import axios from "axios";

import Layout from "../../components/Layout";

function CreateEditPermission({ refreshPermissions, permissionTypes }) {
  const [showSnackBar, setShowSnackBar] = useState(false);
  const [showSnackBarAsError, setShowSnackBarAsError] = useState(false);

  const [employeeFirstName, setEmployeeFirstName] = useState(null);
  const [employeeFirstNameError, setEmployeeFirstNameError] = useState(false);

  const [employeeLastName, setEmployeeLastName] = useState(null);
  const [employeeLastNameError, setEmployeeLastNameError] = useState(false);

  const [permissionType, setPermissionType] = useState(null);
  const [permissionTypeError, setPermissionTypeError] = useState(false);

  const routeParams = useParams();

  useEffect(() => {
    getFormData();
  }, []);

  const getFormData = async () => {
    if (routeParams && routeParams.permissionId !== "-1") {
      const permissionResult = await axios.get(
        `${process.env.REACT_APP_PERMISSIONS_API_URL}/Permissions/${routeParams.permissionId}`
      );
      setEmployeeFirstName(permissionResult.data.employeeFirstName);
      setEmployeeLastName(permissionResult.data.employeeLastName);
      setPermissionType(permissionResult.data.permissionTypeId);
    }
  };

  const save = () => {
    let isFormValid = true;
    if (
      employeeFirstName === null ||
      employeeFirstName === undefined ||
      employeeFirstName === ""
    ) {
      isFormValid = false;
      setEmployeeFirstNameError(true);
    } else {
      setEmployeeFirstNameError(false);
    }

    if (
      employeeLastName === null ||
      employeeLastName === undefined ||
      employeeLastName === ""
    ) {
      isFormValid = false;
      setEmployeeLastNameError(true);
    } else {
      setEmployeeLastNameError(false);
    }

    if (
      permissionType === null ||
      permissionType === undefined ||
      permissionType === ""
    ) {
      isFormValid = false;
      setPermissionTypeError(true);
    } else {
      setPermissionTypeError(false);
    }

    if (isFormValid) {
      savePermission({
        id: parseInt(routeParams.permissionId),
        employeeFirstName,
        employeeLastName,
        permissionTypeId: permissionType,
      });
    }
  };

  const savePermission = async (permission) => {
    try {
      if (permission.id === -1) {
        await axios.post(
          `${process.env.REACT_APP_PERMISSIONS_API_URL}/Permissions`,
          { ...permission, id: 0 }
        );
      } else {
        await axios.put(
          `${process.env.REACT_APP_PERMISSIONS_API_URL}/Permissions/${permission.id}`,
          permission
        );
      }

      await refreshPermissions();

      setShowSnackBarAsError(false);
    } catch {
      setShowSnackBarAsError(true);
    } finally {
      setShowSnackBar(true);
    }
  };

  const deletePermission = async (id) => {
    try {
      await axios.delete(
        `${process.env.REACT_APP_PERMISSIONS_API_URL}/Permissions/${id}`
      );
      await refreshPermissions();

      setShowSnackBarAsError(false);
    } catch {
      setShowSnackBarAsError(true);
    } finally {
      setShowSnackBar(true);
    }
  };

  const handleClose = (_, reason) => {
    if (reason === "clickaway") {
      return;
    }

    setShowSnackBar(false);
  };

  const iconComponent =
    routeParams && routeParams.permissionId !== -1 ? <DeleteIcon /> : null;
  const iconAction = () => deletePermission(routeParams.permissionId);

  return (
    <Layout
      title="Permissions"
      iconComponent={iconComponent}
      iconAction={iconAction}
    >
      <Stack sx={{ width: "100%" }}>
        <Snackbar
          open={showSnackBar}
          autoHideDuration={2000}
          onClose={handleClose}
        >
          {showSnackBarAsError ? (
            <Alert
              elevation={6}
              variant="filled"
              onClose={handleClose}
              severity="error"
              sx={{ width: "100%" }}
            >
              Operation was unsuccessful!
            </Alert>
          ) : (
            <Alert
              elevation={6}
              variant="filled"
              onClose={handleClose}
              severity="success"
              sx={{ width: "100%" }}
            >
              Operation was successful!
            </Alert>
          )}
        </Snackbar>
        <Box
          sx={{
            display: "flex",
            flexDirection: "column",
            alignItems: "center",
            justifyContent: "stretch",
          }}
        >
          <Box sx={{ margin: "20px" }}>
            <TextField
              error={employeeFirstNameError}
              helperText={employeeFirstNameError ? "Field cannot be empty" : ""}
              label="Employee First Name"
              variant="outlined"
              onChange={(event) => setEmployeeFirstName(event.target.value)}
              value={employeeFirstName || ""}
            />
          </Box>
          <Box sx={{ margin: "20px" }}>
            <TextField
              error={employeeLastNameError}
              helperText={employeeLastNameError ? "Field cannot be empty" : ""}
              label="Employee Last Name"
              variant="outlined"
              onChange={(event) => setEmployeeLastName(event.target.value)}
              value={employeeLastName || ""}
            />
          </Box>
          <Box sx={{ margin: "20px", minWidth: "200px" }}>
            <FormControl fullWidth error={permissionTypeError}>
              <InputLabel id="type-select-label">Type</InputLabel>
              <Select
                labelId="type-select-label"
                label="Type"
                value={permissionType || ""}
                onChange={(event) => setPermissionType(event.target.value)}
              >
                {permissionTypes.map((type) => (
                  <MenuItem key={type.id} value={type.id}>
                    {type.description}
                  </MenuItem>
                ))}
              </Select>
              {permissionTypeError && (
                <FormHelperText>Field cannot be empty</FormHelperText>
              )}
            </FormControl>
          </Box>
          <Box sx={{ margin: "20px" }}>
            <Button
              variant="contained"
              onClick={save}
              sx={{ padding: "8px", minWidth: "200px" }}
            >
              Save
            </Button>
          </Box>
        </Box>
      </Stack>
    </Layout>
  );
}

export default CreateEditPermission;
