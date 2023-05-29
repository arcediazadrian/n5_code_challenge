import React, { useState } from "react";

import Box from '@mui/material/Box';

import AppBar from "./AppBar";
import Drawer from "./Drawer";

function Layout({ title, iconComponent, iconAction, children }) {
    const [openDrawer, setOpenDrawer] = useState(false);
    const toggleDrawer =
        (open) =>
            (event) => {
                if (
                    event.type === 'keydown' &&
                    ((event).key === 'Tab' ||
                        (event).key === 'Shift')
                ) {
                    return;
                }

                setOpenDrawer(open);
            };
    return (
        <Box sx={{ height: '100vh', width: '100vw' }}>
            <Box>
                <Drawer openDrawer={openDrawer} toggleDrawer={toggleDrawer} />
                <AppBar title={title} toggleDrawer={toggleDrawer} iconComponent={iconComponent} iconAction={iconAction} />
            </Box>
            <Box>
                {children}
            </Box>
        </Box>
    );
}

export default Layout;
