import { Button, TextField, Typography } from "@mui/material";
import { makeStyles } from "@mui/styles";
import { Box } from "@mui/system";
import React, {useState} from "react";

const useStyles = makeStyles({
    form: {
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center',
        flexDirection: 'column',
        gap: '20px',
        padding: '10px'
    }
  });

export const LoginForm = () => {
    const classes = useStyles();

    const [formData, setFormData] = useState({
        email: '',
        password: '',
    });


    const handleChange = (event) => {
        setFormData({
            ...formData,
            [event.target.name]: event.target.value
        })
        console.log(event.target.value)
    };

    return (
        <Box className={classes.form}>
            <Typography 
                gutterBottom
            >
                Login
            </Typography>
            <TextField
                name={"email"}
                onChange={handleChange}
                label={"Email"}
                variant="outlined"
                required
            />
            <TextField
                name={"password"}
                onChange={handleChange}
                label={"Password"}
                variant="outlined"
                required
            />
            <Button 
                variant="outlined"
                fullWidth    
            >
                Sign Up
            </Button>
        </Box>
    )
}