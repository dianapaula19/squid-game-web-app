import { 
    Autocomplete, 
    Button, 
    ButtonGroup, 
    FormControl, 
    FormControlLabel, 
    FormLabel, 
    Radio, 
    RadioGroup, 
    TextField, 
    Typography } 
from "@mui/material";
import { makeStyles } from "@mui/styles";
import { Box } from "@mui/system";
import React, {useState} from "react";
import { countryList } from "../../../../../Utils";

const useStyles = makeStyles({

    root: {
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center',
        flexDirection: 'row',
        maxWidth: '40%',
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

enum Role {
    player = 'Player',
    guard = 'Guard',
}

export const RegistrationForm = () => {
    const classes = useStyles();

    const [formData, setFormData] = useState({
        username: '',
        email: '',
        country: '',
        firstName: '',
        lastName: '',
        gender: '',
        password: '',
        confirmPassword: ''
    });

    const [role, setRole] = useState<Role | null>(null);

    const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setFormData({
            ...formData,
            [event.target.name]: event.target.value
        })
        console.log(event.target.value)
    };

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
                        required
                        fullWidth
                    />
                    <TextField
                        className={classes.textfield}
                        name={"email"}
                        onChange={handleChange}
                        label={"Email"}
                        variant="outlined"
                        required
                        fullWidth
                    />
                    <Autocomplete
                        disablePortal
                        id="combo-box-country"
                        options={countryList}
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
                                    required
                                    fullWidth
                                />
                                <TextField
                                    className={classes.textfield}
                                    name={"lastName"}
                                    label={"Last Name"}
                                    variant="outlined"
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
                                            <FormControlLabel value="other" control={<Radio />} label="Other" />
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
                        variant="outlined"
                        required
                        fullWidth
                    />
                    <TextField
                        className={classes.textfield}
                        name={"confirmPassword"}
                        onChange={handleChange}
                        label={"Confirm Password"}
                        variant="outlined"
                        required
                        fullWidth
                    />
                    <Button 
                        variant="contained"
                        fullWidth    
                    >
                        Sign Up
                    </Button>
                </Box>
            )}
        </Box>
    )
}