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
import { countryList } from "../../../../Utils";

const useStyles = makeStyles({

    root: {
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center',
        flexDirection: 'row',
    },
    buttons: {
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center',
        flexDirection: 'column',
    },
    blueButton: {
        backgroundColor: '#3d8b97',
        color: '#3d8b97'
    },
    redButton: {
        backgroundColor: '#b32c6f'
    },
    form: {
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center',
        flexDirection: 'column',
        gap: '20px',
        padding: '10px'
    }
  });

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

    const [role, setRole] = useState(null);

    const handleChange = (event) => {
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
                    <Typography>
                        Pick a colour
                    </Typography>
                    <ButtonGroup aria-label="outlined primary button group">
                        <Button 
                            onClick={() => setRole('PLAYER')}
                            className={classes.blueButton}
                        >
                            Blue
                        </Button>
                        <Button 
                            className={classes.redButton}
                            onClick={() => setRole('GUARD')}
                        >
                            Red
                        </Button>
                    </ButtonGroup>
                </Box>
            )}
            {role !== null && (
                <Box className={classes.form}>
                    <Typography 
                        gutterBottom
                    >
                        Register for an Account
                    </Typography>
                    <TextField
                        name={"username"}
                        onChange={handleChange}
                        label={"Username"}
                        variant="outlined"
                        required
                    />
                    <TextField
                        name={"email"}
                        onChange={handleChange}
                        label={"Email"}
                        variant="outlined"
                        required
                    />
                    <Autocomplete
                        disablePortal
                        id="combo-box-country"
                        options={countryList}
                        onChange={handleChange}
                        fullWidth
                        required
                        renderInput={(params) => <TextField {...params} label="Country" />}
                    />
                    {
                        role === 'PLAYER' && (
                            <>  
                                <TextField
                                    name={"firstName"}
                                    label={"First Name"}
                                    variant="outlined"
                                    required
                                />
                                <TextField
                                    name={"lastName"}
                                    label={"Last Name"}
                                    variant="outlined"
                                    required
                                />
                                <FormControl>
                                    <FormLabel id="gender-radio-buttons-group-label">Gender</FormLabel>
                                        <RadioGroup
                                            row
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
                        name={"password"}
                        onChange={handleChange}
                        label={"Password"}
                        variant="outlined"
                        required
                    />
                    <TextField
                        name={"confirmPassword"}
                        onChange={handleChange}
                        label={"Confirm Password"}
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
            )}
        </Box>
    )
}