import { Box, Button, FormControl, FormControlLabel, FormLabel, Radio, RadioGroup, TextField, Typography } from "@mui/material";
import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { authState } from "../../../features/auth/authSlice";
import { userProfileAsync } from "../../../features/user/userSlice";
import { Role } from "../../../Utils";

export interface IProps {
    role: Role;
}

export const ProfilePage = ({role}: IProps) => {
    const {token, email} = useSelector(authState);

    const dispatch = useDispatch();

    const [userData, setUserData] = useState(null);

    const [formData, setFormData] = useState({
        firstName: "",
        lastName: "",
        gender: "",
    });

    const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setFormData({
            ...formData,
            [event.target.name]: event.target.value
        });
    };

    useEffect(() => {
        if (userData === null) {
            dispatch(userProfileAsync({token: token, email: email}));
        }
    }, []);
    

    return(
        <Box>
            {
                role === Role.player && (
                    <Box>
                        <TextField
                            name={"firstName"}
                            label={"First Name"}
                            variant="outlined"
                            onChange={handleChange}
                            required
                            fullWidth
                        />
                        <TextField
                            name={"lastName"}
                            label={"Last Name"}
                            variant="outlined"
                            onChange={handleChange}
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
                        <Button>

                        </Button>
                    </Box>       
                )
            }
            {
                role === Role.guard && (
                    <Box>
                        <Typography
                            gutterBottom
                        >
                        </Typography>
                    </Box>       
                )
            }
            {
                role === Role.frontman && (
                    <Box>
                        
                    </Box>       
                )
            }
            <Typography>
                {email}
            </Typography>
            <Button
                variant="outlined"
                color="error"
            >
                Delete Account
            </Button>

        </Box>
    )
}