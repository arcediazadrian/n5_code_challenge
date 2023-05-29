import React from "react";

import Box from '@mui/material/Box';
import { Typography } from "@mui/material";
import MuiAppBar from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import IconButton from '@mui/material/IconButton';
import MenuIcon from '@mui/icons-material/Menu';

function AppBar({ title, toggleDrawer, iconComponent, iconAction }) {
    return (
        <MuiAppBar position="static">
            <Toolbar>
                <IconButton
                    size="large"
                    edge="start"
                    aria-label="menu"
                    position="absolute"
                    color="inherit"
                    sx={{ mr: 2 }}
                    onClick={toggleDrawer(true)}
                >
                    <MenuIcon />
                </IconButton>
                <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
                    {title}
                </Typography>
                {iconComponent && (<Box>
                    <IconButton
                        size="large"
                        onClick={iconAction}
                        color="inherit"
                    >
                        {iconComponent}
                    </IconButton>

                </Box>)}
            </Toolbar>
        </MuiAppBar>
    );
}

export default AppBar;
