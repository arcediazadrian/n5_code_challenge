import React from "react";

import { Typography } from "@mui/material";
import { styled } from "@mui/material/styles";
import Box from "@mui/material/Box";
import Paper from "@mui/material/Paper";
import EditIcon from "@mui/icons-material/Edit";
import IconButton from "@mui/material/IconButton";

import { useNavigate } from "react-router-dom";

const Item = styled(Paper)(({ theme }) => ({
  backgroundColor: theme.palette.mode === "dark" ? "#1A2027" : "#fff",
  ...theme.typography.body2,
  padding: theme.spacing(3),
  color: theme.palette.text.secondary,
  elevation: 3,
  margin: "20px",
}));

function PermissionTypesList({ permissionTypes }) {
  const navigate = useNavigate();

  const navigateToEdit = (id) => {
    navigate(`/permissionTypes/${id}`);
  };

  return (
    <Box
      sx={{
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
        justifyContent: "stretch",
      }}
    >
      <Box sx={{ width: "60vw" }}>
        {permissionTypes.map((permissionType, index) => (
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
                  {permissionType.description}
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
                  onClick={() => navigateToEdit(permissionType.id)}
                >
                  <EditIcon />
                </IconButton>
              </Box>
            </Box>
          </Item>
        ))}
      </Box>
    </Box>
  );
}

export default PermissionTypesList;
