import { Button, TextField, Typography } from "@mui/material";
import { makeStyles } from "@mui/styles";
import { Box } from "@mui/system";
import React, {useState} from "react";
import { useDispatch } from "react-redux";
import { ILoginRequest, loginAsync } from "../../../../features/auth/authSlice";

const useStyles = makeStyles({
    form: {
        // backgroundColor: 'rgb(255, 255, 255)',
        maxWidth: '40%',
        maxHeight: '50%',
        marginRight: '20%',
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center',
        flexDirection: 'column',
        gap: '20px',
        padding: '10px'
    },
    textfield: {
        backgroundColor: 'white'
    }
  });


export const LoginForm = () => {
    const classes = useStyles();

    const dispatch = useDispatch();

    const [formData, setFormData] = useState<ILoginRequest>({
        email: '',
        password: '',
    });


    const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setFormData({
            ...formData,
            [event.target.name]: event.target.value
        });
    };

    const signIn = () => {
        dispatch(loginAsync(formData));
    }

    return (
        <Box className={classes.form}>
            <Typography 
                variant="h4"
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
                fullWidth
                className={classes.textfield}
            />
            <TextField
                name={"password"}
                onChange={handleChange}
                label={"Password"}
                variant="outlined"
                required
                fullWidth
                type={"password"}
                className={classes.textfield}
            />
            <Button 
                variant="contained"
                fullWidth
                onClick={signIn}
            >
                Sign In
            </Button>
        </Box>
    )
}