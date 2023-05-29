import React, { useEffect, useState } from "react";

import { TextField, Button } from "@mui/material";
import { Box } from "@mui/system";
import DeleteIcon from "@mui/icons-material/Delete";
import Stack from "@mui/material/Stack";
import Snackbar from "@mui/material/Snackbar";
import Alert from "@mui/material/Alert";
import { useParams } from "react-router-dom";
import axios from "axios";

import Layout from "../../components/Layout";

function CreateEditPermissionType({ refreshPermissionTypes }) {
  const [showSnackBar, setShowSnackBar] = useState(false);
  const [showSnackBarAsError, setShowSnackBarAsError] = useState(false);

  const [description, setDescription] = useState(null);
  const [descriptionError, setDescriptionError] = useState(false);

  const routeParams = useParams();

  useEffect(() => {
    getFormData();
  }, []);

  const getFormData = async () => {
    if (routeParams && routeParams.permissionTypeId !== "-1") {
      const permissionResult = await axios.get(
        `${process.env.REACT_APP_PERMISSIONS_API_URL}/PermissionTypes/${routeParams.permissionTypeId}`
      );
      setDescription(permissionResult.data.description);
    }
  };

  const save = () => {
    let isFormValid = true;
    if (
      description === null ||
      description === undefined ||
      description === ""
    ) {
      isFormValid = false;
      setDescriptionError(true);
    } else {
      setDescriptionError(false);
    }

    if (isFormValid) {
      savePermissionType({
        id: parseInt(routeParams.permissionTypeId),
        description,
      });
    }
  };

  const savePermissionType = async (permissionType) => {
    try {
      if (permissionType.id === -1) {
        await axios.post(
          `${process.env.REACT_APP_PERMISSIONS_API_URL}/PermissionTypes`,
          { ...permissionType, id: 0 }
        );
      } else {
        await axios.put(
          `${process.env.REACT_APP_PERMISSIONS_API_URL}/PermissionTypes/${permissionType.id}`,
          permissionType
        );
      }

      await refreshPermissionTypes();

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
        `${process.env.REACT_APP_PERMISSIONS_API_URL}/PermissionTypes/${id}`
      );
      await refreshPermissionTypes();

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
    routeParams && routeParams.permissionTypeId !== -1 ? <DeleteIcon /> : null;
  const iconAction = () => deletePermission(routeParams.permissionTypeId);

  return (
    <Layout
      title="Permission Types"
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
              error={descriptionError}
              helperText={descriptionError ? "Field cannot be empty" : ""}
              label="Description"
              variant="outlined"
              onChange={(event) => setDescription(event.target.value)}
              value={description || ""}
            />
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

export default CreateEditPermissionType;
