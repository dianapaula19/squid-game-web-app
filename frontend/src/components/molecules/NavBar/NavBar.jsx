import { AppBar, Button, IconButton, Menu, MenuItem, Toolbar, Typography } from "@mui/material";
import { Box } from "@mui/system";
import React from "react";
import MenuIcon from '@mui/icons-material/Menu';
import { links } from "../../../Utils";
import { Link} from "react-router-dom";

export const NavBar = ({role, ...props}) => {
    const [anchorEl, setAnchorEl] = React.useState(null);
    const open = Boolean(anchorEl);
    const handleClick = (event) => {
      setAnchorEl(event.currentTarget);
    };
    const handleClose = () => {
      setAnchorEl(null);
    };
    
    return (
        <Box sx={{ flexGrow: 1 }}>
            <AppBar position="static">
                <Toolbar>
                    {
                        role !== 'UNDEFINED' && (
                            <>
                                <IconButton
                                onClick={handleClick}
                                size="large"
                                edge="start"
                                color="inherit"
                                aria-label="menu"
                                sx={{ mr: 2 }}
                                aria-controls={open ? 'account-menu' : undefined}
                                aria-haspopup="true"
                                aria-expanded={open ? 'true' : undefined}
                                >
                                    <MenuIcon />
                                </IconButton>
                                <Menu
                                    anchorEl={anchorEl}
                                    id="account-menu"
                                    open={open}
                                    onClose={handleClose}
                                    onClick={handleClose}
                                    PaperProps={{
                                    elevation: 0,
                                    sx: {
                                        overflow: 'visible',
                                        filter: 'drop-shadow(0px 2px 8px rgba(0,0,0,0.32))',
                                        mt: 1.5,
                                        '& .MuiAvatar-root': {
                                        width: 32,
                                        height: 32,
                                        ml: -0.5,
                                        mr: 1,
                                        },
                                        '&:before': {
                                        content: '""',
                                        display: 'block',
                                        position: 'absolute',
                                        top: 0,
                                        right: 14,
                                        width: 10,
                                        height: 10,
                                        bgcolor: 'background.paper',
                                        transform: 'translateY(-50%) rotate(45deg)',
                                        zIndex: 0,
                                        },
                                    },
                                    }}
                                    transformOrigin={{ horizontal: 'right', vertical: 'top' }}
                                    anchorOrigin={{ horizontal: 'right', vertical: 'bottom' }}
                                >
                                    <MenuItem>
                                        Profile
                                    </MenuItem>
                                    {
                                        links[role].map((link, index) => (
                                            <MenuItem
                                                key = {link + index}
                                                component={Link} 
                                                to={"/" + link.toLowerCase()}
                                            >
                                                {link}
                                            </MenuItem>
                                        ))
                                    }
                                </Menu>
                            </>
                        )
                    }
                    <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
                        Squid Game Web App
                    </Typography>
                    {
                        role === 'UNDEFINED' ? 
                    (
                        <>
                            <Button color="inherit">Sign In</Button>
                            <Button color="inherit">Sign Up</Button>    
                        </>
                    ) 
                    : 
                    (
                        <Button color="inherit">Sign Out</Button>
                    ) 
                    }
                </Toolbar>
            </AppBar>
        </Box>
    )
}