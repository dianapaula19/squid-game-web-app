import { Box, Button, TextField, Typography } from "@mui/material";
import React, { useEffect, useState } from "react";
import { useSelector } from "react-redux";
import { useAppSelector } from "../../../app/hooks";
import { authState } from "../../../features/auth/authSlice";
import { userState } from "../../../features/user/userSlice";
import { guardMessage, GuardRole, Role } from "../../../Utils";

export interface IProps {
    role: Role;
}

export interface IUserData {
    username: string | null;
    country: string | null;
    firstName?: string | null;
    lastName?: string | null;
    gender?: string | null;
    type?: GuardRole | null;
}


export const ProfilePage = ({role}: IProps) => {
    const { email } = useSelector(authState);

    const { 
        username, 
        country, 
        firstName, 
        lastName, 
        gender, 
        type, 
        isSuccess
    } = useAppSelector(userState);

    const [userData, setUserData] = useState<IUserData | null>(null);

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
        if (userData === null && isSuccess) {
            switch (role) {
                case Role.player:
                    setUserData({
                        username: username,
                        country: country,
                        firstName: firstName,
                        lastName: lastName,
                        gender: gender
                    });    
                    break;
                case Role.guard:
                    setUserData({
                        username: username,
                        country: country,
                        type: type
                    });
                    break;
                default:
                    setUserData({
                        username: username,
                        country: country
                    });
                    break;
            }
        }
    }, [userData, isSuccess, role, username, country, firstName, lastName, gender, type]);

    return(
        <Box>
            <Typography
                gutterBottom
            >
                Email: {email}
            </Typography>
            {
                role === Role.player && (
                    <Box>
                        <TextField
                            name={"firstName"}
                            label={"First Name"}
                            variant="outlined"
                            onChange={handleChange}
                            defaultValue={userData?.firstName !== null ? userData?.firstName : "An error has occured"}
                            required
                            fullWidth
                        />
                        <TextField
                            name={"lastName"}
                            label={"Last Name"}
                            variant="outlined"
                            onChange={handleChange}
                            defaultValue={userData?.lastName !== null ? userData?.lastName : "An error has occured"}
                            required
                            fullWidth
                        />
                        <TextField
                            name={"gender"}
                            label={"Gender"}
                            variant="outlined"
                            onChange={handleChange}
                            defaultValue={userData?.gender !== null ? userData?.gender : "An error has occured"}
                            disabled
                            fullWidth
                        />
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
                        {
                            (userData?.type !== null && userData?.type !== undefined) ? guardMessage(userData?.type) : "An error has occured"
                        }
                        </Typography>
                    </Box>       
                )
            }
            {
                role === Role.frontman && (
                    <Typography>
                        You are a FrontMan from {userData?.country}
                    </Typography>       
                )
            }
            <Button
                variant="outlined"
                color="error"
            >
                Delete Account
            </Button>

        </Box>
    )
}