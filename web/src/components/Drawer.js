import React from "react";

import Box from '@mui/material/Box';
import List from '@mui/material/List';
import Divider from '@mui/material/Divider';
import ListItem from '@mui/material/ListItem';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemIcon from '@mui/material/ListItemIcon';
import ListItemText from '@mui/material/ListItemText';
import MuiDrawer from '@mui/material/Drawer';
import ListAltIcon from '@mui/icons-material/ListAlt';
import AddIcon from '@mui/icons-material/Add';
import ListSubheader from '@mui/material/ListSubheader';
import { useNavigate } from "react-router-dom";

function Drawer({ openDrawer, toggleDrawer }) {
    const navigate = useNavigate();

    return (
        <MuiDrawer
            anchor="left"
            open={openDrawer}
            onClose={toggleDrawer(false)}
        >
            <Box
                sx={{ width: 250 }}
                role="presentation"
                onClick={toggleDrawer(false)}
                onKeyDown={toggleDrawer(false)}
            >
                <List subheader={<ListSubheader>Permissions</ListSubheader>}>
                    <ListItem key={'List'} disablePadding>
                        <ListItemButton onClick={() => navigate('/permissions')}>
                            <ListItemIcon>
                                <ListAltIcon />
                            </ListItemIcon>
                            <ListItemText primary={'List'} />
                        </ListItemButton>
                    </ListItem>
                    <ListItem key={'Create'} disablePadding>
                        <ListItemButton onClick={() => navigate('/permissions/-1')}>
                            <ListItemIcon>
                                <AddIcon />
                            </ListItemIcon>
                            <ListItemText primary={'Create'} />
                        </ListItemButton>
                    </ListItem>
                </List>
                <Divider />
                <List subheader={<ListSubheader>Permission Types</ListSubheader>}>
                    <ListItem key={'List'} disablePadding>
                        <ListItemButton onClick={() => navigate('/permissionTypes')}>
                            <ListItemIcon>
                                <ListAltIcon />
                            </ListItemIcon>
                            <ListItemText primary={'List'} />
                        </ListItemButton>
                    </ListItem>
                    <ListItem key={'Create'} disablePadding>
                        <ListItemButton onClick={() => navigate('/permissionTypes/-1')}>
                            <ListItemIcon>
                                <AddIcon />
                            </ListItemIcon>
                            <ListItemText primary={'Create'} />
                        </ListItemButton>
                    </ListItem>
                </List>
            </Box>
        </MuiDrawer>
    );
}

export default Drawer;
