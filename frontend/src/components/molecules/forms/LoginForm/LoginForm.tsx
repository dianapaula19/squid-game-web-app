import { Button, TextField, Typography } from "@mui/material";
import { makeStyles } from "@mui/styles";
import { Box } from "@mui/system";
import React, {useState} from "react";
import { useDispatch } from "react-redux";
import { ILoginRequest, loginAsync } from "../../../../features/auth/authSlice";
import { emailRegex, passwordRegex } from "../../../../Utils";

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
        Email: '',
        Password: '',
    });

    const errors = {
        email: {
            valid: emailRegex.test(formData.Email),
            errorMessage: (
                <div>
                    <div>The email must follow the following pattern:</div>
                    <div>example@domain.com</div>    
                </div>
            )
        },
        password: {
            valid: passwordRegex.test(formData.Password),
            errorMessage: (
                <div>
                    <div>The password must have minimum eight characters,</div>
                    <div>at least one letter, one number and one special character</div>    
                </div>
            )
        },
    }


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
                name={"Email"}
                onChange={handleChange}
                label={"Email"}
                variant="outlined"
                error={!errors.email.valid}
                helperText={
                    !errors.email.valid && 
                    errors.email.errorMessage
                }
                required
                fullWidth
                className={classes.textfield}
            />
            <TextField
                name={"Password"}
                onChange={handleChange}
                label={"Password"}
                variant="outlined"
                error={!errors.password.valid}
                helperText={
                    !errors.password.valid && 
                    errors.password.errorMessage
                }
                required
                fullWidth
                type={"password"}
                className={classes.textfield}
            />
            <Button 
                variant="contained"
                fullWidth
                onClick={signIn}
                disabled={!(
                    errors.email.valid &&
                    errors.password.valid
                )}
            >
                Sign In
            </Button>
        </Box>
    )
}