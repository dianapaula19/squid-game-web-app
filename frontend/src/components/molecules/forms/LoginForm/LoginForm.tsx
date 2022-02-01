import { Visibility, VisibilityOff } from "@mui/icons-material";
import { Button, IconButton, InputAdornment, Modal, TextField, Typography } from "@mui/material";
import { makeStyles } from "@mui/styles";
import { Box } from "@mui/system";
import React, {useState} from "react";
import { useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";
import { useAppSelector } from "../../../../app/hooks";
import { authState, ILoginRequest, loginAsync } from "../../../../features/auth/authSlice";
import { userProfileAsync } from "../../../../features/user/userSlice";
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

    let navigate = useNavigate();

    const classes = useStyles();

    const dispatch = useDispatch();

    const [open, setOpen] = useState(false);
    const handleOpen = () => setOpen(true);
    const handleClose = () => setOpen(false);

    const [showPassword, setShowPassword] = useState(false);
    const handleClickShowPassword = () => setShowPassword(!showPassword);
    const handleMouseDownPassword = () => setShowPassword(!showPassword);

    const {token, email, isError} = useAppSelector(authState);

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
        if (isError === true) {
            handleOpen();
            return;
        }
        dispatch(userProfileAsync({token: token, email: email}));
        navigate("/profile");
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
                type={showPassword ? "text" : "password"}
                InputProps={{
                    endAdornment: (
                      <InputAdornment position="end">
                        <IconButton
                          aria-label="toggle password visibility"
                          onClick={handleClickShowPassword}
                          onMouseDown={handleMouseDownPassword}
                        >
                          {showPassword ? <Visibility /> : <VisibilityOff />}
                        </IconButton>
                      </InputAdornment>
                    )
                  }}
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
            <Modal
                open={open}
                onClose={handleClose}
                aria-labelledby="modal-modal-title"
                aria-describedby="modal-modal-description"
            >
                <Box sx={{
                    position: 'absolute' as 'absolute',
                    top: '50%',
                    left: '50%',
                    transform: 'translate(-50%, -50%)',
                    width: 400,
                    bgcolor: 'white',
                    border: '2px solid #000',
                    boxShadow: 24,
                    p: 4,
                    }}
                >
                    <Typography id="modal-modal-title" variant="h6" component="h2">
                        Error
                    </Typography>
                    <Typography id="modal-modal-description" sx={{ mt: 2 }}>
                        An error has occured. Try again!
                    </Typography>
                </Box>
             </Modal>
        </Box>
    )
}