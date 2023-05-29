import React from "react";

import { Typography } from "@mui/material";
import { styled } from "@mui/material/styles";
import Box from "@mui/material/Box";
import Paper from "@mui/material/Paper";
import EditIcon from "@mui/icons-material/Edit";
import IconButton from "@mui/material/IconButton";
import Layout from "../../components/Layout";

import { useNavigate } from "react-router-dom";

const Item = styled(Paper)(({ theme }) => ({
  backgroundColor: theme.palette.mode === "dark" ? "#1A2027" : "#fff",
  ...theme.typography.body2,
  padding: theme.spacing(3),
  color: theme.palette.text.secondary,
  elevation: 3,
  margin: "20px",
}));

function PermissionsList({ permissions }) {
  const navigate = useNavigate();

  const navigateToEdit = (id) => {
    navigate(`/permissions/${id}`);
  };

  return (
    <Layout title="Permissions">
      <Box
        sx={{
          display: "flex",
          flexDirection: "column",
          alignItems: "center",
          justifyContent: "stretch",
        }}
      >
        <Box sx={{ width: "60vw" }}>
          {permissions.map((permission, index) => (
            <Item key={index}>
              <Box
                sx={{
                  display: "flex",
                  flexDirection: "row",
                  justifyContent: "space-between",
                  alignItems: "center",
                }}
              >
                <Box sx={{ flexGrow: 6 }}>
                  <Typography sx={{ fontSize: 32 }} color="text.primary">
                    {`${permission.employeeFirstName} ${permission.employeeLastName}`}
                  </Typography>
                  <Typography variant="h5">
                    {permission.permissionType.description}
                  </Typography>
                  <Typography sx={{ mb: 1.5 }} color="text.secondary">
                    {permission.grantedDate}
                  </Typography>
                </Box>
                <Box
                  sx={{
                    flexGrow: 1,
                    display: "flex",
                    flexDirection: "row",
                    justifyContent: "flex-end",
                    marginRight: "20px",
                  }}
                >
                  <IconButton
                    size="large"
                    onClick={() => navigateToEdit(permission.id)}
                  >
                    <EditIcon />
                  </IconButton>
                </Box>
              </Box>
            </Item>
          ))}
        </Box>
      </Box>
    </Layout>
  );
}

export default PermissionsList;
