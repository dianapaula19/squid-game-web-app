import { Visibility, VisibilityOff } from "@mui/icons-material";
import { 
    Autocomplete, 
    Button, 
    ButtonGroup, 
    FormControl, 
    FormControlLabel, 
    FormLabel, 
    IconButton, 
    InputAdornment, 
    Radio, 
    RadioGroup, 
    TextField, 
    Typography } 
from "@mui/material";
import { makeStyles } from "@mui/styles";
import { Box } from "@mui/system";
import React, {useState} from "react";
import { useDispatch } from "react-redux";
import { registerAsync } from "../../../../features/auth/authSlice";
import { countryList, emailRegex, passwordRegex, Role } from "../../../../Utils";

const useStyles = makeStyles({

    root: {
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center',
        flexDirection: 'row',
        maxWidth: '40%',
        marginTop: '10%',
        marginRight: '20%',
    },
    buttons: {
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center',
        flexDirection: 'column'
    },
    blueButton: {
        backgroundColor: '#3d8b97',
        maxHeight: '10%',
        color: 'white',
        '&:hover': {
            color: 'black'
        }
    },
    redButton: {
        backgroundColor: '#b32c6f',
        color: 'white',
        minWidth: '20%',
        '&:hover': {
            color: 'black'
        }
    },
    form: {
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center',
        flexDirection: 'column',
        gap: '20px',
        padding: '10px',
    },
    textfield: {
        backgroundColor: 'white'
    }
  });

export const RegistrationForm = () => {

    const dispatch = useDispatch();
    
    const classes = useStyles();

    const [formData, setFormData] = useState({
        username: "",
        email: "",
        country: "",
        firstName: "",
        lastName: "",
        gender: "",
        password: "",
        confirmPassword: ""
    });

    const [showPassword, setShowPassword] = useState(false);
    const handleClickShowPassword = () => setShowPassword(!showPassword);
    const handleMouseDownPassword = () => setShowPassword(!showPassword);

    const [showConfirmPassword, setShowConfirmPassword] = useState(false);
    const handleClickShowConfirmPassword = () => setShowConfirmPassword(!showConfirmPassword);
    const handleMouseDownConfirmPassword = () => setShowConfirmPassword(!showConfirmPassword);

    const errors = {
        username: {
            valid: formData.username !== "",
            errorMessage: "The username field must not be empty" 
        },
        email: {
            valid: emailRegex.test(formData.email),
            errorMessage: (
                <div>
                    <div>The email must follow the following pattern:</div>
                    <div>example@domain.com</div>    
                </div>
            )
        },
        country: {
            valid: formData.country !== "",
            errorMessage: "The country field must not be empty"
        },
        firstName: {
            valid: formData.firstName !== "",
            errorMessage: "The first name field must not be empty"
        },
        lastName: {
            valid: formData.lastName !== "",
            errorMessage: "The last name field must not be empty"
        },
        gender: {
            valid: formData.gender !== "",
            errorMessage: "The gender field must not be empty"
        },
        password: {
            valid: passwordRegex.test(formData.password),
            errorMessage: (
                <div>
                    <div>The password must have minimum eight characters,</div>
                    <div>at least one letter, one number and one special character</div>    
                </div>
            )
        },
        confirmPassword: {
            valid: formData.password === formData.confirmPassword,
            errorMessage: "The passwords don't match"
        },
    };

    const [role, setRole] = useState<Role | null>(null);

    const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setFormData({
            ...formData,
            [event.target.name]: event.target.value
        });
    };

    const signUp = () => {
        if (role === Role.player) {
            const request = {
                Username: formData.username,
                Email: formData.email,
                Password: formData.password,
                Role: Role.player,
                Country: formData.country,
                PlayerInfo: {
                    FirstName: formData.firstName,
                    LastName: formData.lastName,
                    Gender: formData.gender
                }    
            }
            dispatch(registerAsync(request));
            
        } else {
            const request = {
                Username: formData.username,
                Email: formData.email,
                Password: formData.password,
                Role: Role.guard,
                Country: formData.country, 
            }
            dispatch(registerAsync(request));
        }
    }

    return (
        <Box className={classes.root}>
            {role === null && (
                <Box className={classes.buttons}>
                    <ButtonGroup size="large">
                        <Button 
                            onClick={() => setRole(Role.player)}
                            className={classes.blueButton}
                        >
                            Green
                        </Button>
                        <Button 
                            className={classes.redButton}
                            onClick={() => setRole(Role.guard)}
                        >
                            Red
                        </Button>
                    </ButtonGroup>
                </Box>
            )}
            {role !== null && (
                <Box className={classes.form}>
                    <Typography 
                        variant="h4"
                        gutterBottom
                    >
                        Register for an Account
                    </Typography>
                    <TextField
                        className={classes.textfield}
                        name={"username"}
                        onChange={handleChange}
                        label={"Username"}
                        variant="outlined"
                        error={!errors.username.valid}
                        helperText={
                            !errors.username.valid && 
                            errors.username.errorMessage
                        }
                        required
                        fullWidth
                    />
                    <TextField
                        className={classes.textfield}
                        name={"email"}
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
                    />
                    <Autocomplete
                        disablePortal
                        id="combo-box-country"
                        options={countryList}
                        defaultValue={countryList[0]}
                        fullWidth
                        renderInput={(params) => <TextField 
                                                    {...params}
                                                    className={classes.textfield} 
                                                    label="Country" 
                                                    onChange={handleChange}
                                                    required
                                                    fullWidth 
                                                />}
                    />
                    {
                        role === Role.player && (
                            <>  
                                <TextField
                                    className={classes.textfield}
                                    name={"firstName"}
                                    label={"First Name"}
                                    variant="outlined"
                                    onChange={handleChange}
                                    error={!errors.firstName.valid}
                                    helperText={
                                        !errors.firstName.valid && 
                                        errors.firstName.errorMessage
                                    }
                                    required
                                    fullWidth
                                />
                                <TextField
                                    className={classes.textfield}
                                    name={"lastName"}
                                    label={"Last Name"}
                                    variant="outlined"
                                    onChange={handleChange}
                                    error={!errors.lastName.valid}
                                    helperText={
                                        !errors.lastName.valid && 
                                        errors.lastName.errorMessage
                                    }
                                    required
                                    fullWidth
                                />
                                <FormControl>
                                    <FormLabel id="gender-radio-buttons-group-label">Gender</FormLabel>
                                        <RadioGroup
                                            aria-labelledby="gender-radio-buttons-group-label"
                                            defaultValue="female"
                                            name="gender"
                                            onChange={handleChange}
                                        >
                                            <FormControlLabel value="female" control={<Radio />} label="Female" />
                                            <FormControlLabel value="male" control={<Radio />} label="Male" />
                                            <FormControlLabel value="unspecified" control={<Radio />} label="I'd rather not say" />
                                        </RadioGroup>
                                </FormControl>
                            </>
                        )
                    }
                    <TextField
                        className={classes.textfield}
                        name={"password"}
                        onChange={handleChange}
                        label={"Password"}
                        type={showPassword ? "text" : "password"}
                        variant="outlined"
                        error={!errors.password.valid}
                        helperText={
                            !errors.password.valid && 
                            errors.password.errorMessage
                        }
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
                        required
                        fullWidth
                    />
                    <TextField
                        className={classes.textfield}
                        name={"confirmPassword"}
                        onChange={handleChange}
                        label={"Confirm Password"}
                        type={showConfirmPassword ? "text" : "password"}
                        variant="outlined"
                        error={!errors.confirmPassword.valid}
                        helperText={
                            !errors.confirmPassword.valid && 
                            errors.confirmPassword.errorMessage
                        }
                        required
                        fullWidth
                        InputProps={{
                            endAdornment: (
                              <InputAdornment position="end">
                                <IconButton
                                  aria-label="toggle password visibility"
                                  onClick={handleClickShowConfirmPassword}
                                  onMouseDown={handleMouseDownConfirmPassword}
                                >
                                  {showConfirmPassword ? <Visibility /> : <VisibilityOff />}
                                </IconButton>
                              </InputAdornment>
                            )
                          }}
                    />
                    <Button 
                        variant="contained"
                        onClick={signUp}
                        disabled={!(
                            errors.username.valid && 
                            errors.email.valid &&
                            (
                                role !== Role.player ||
                                (
                                    errors.firstName.valid &&
                                    errors.lastName.valid
                                )
                            ) &&
                            errors.password.valid && 
                            errors.confirmPassword.valid
                        )}
                        fullWidth    
                    >
                        Sign Up
                    </Button>
                </Box>
            )}
        </Box>
    )
}