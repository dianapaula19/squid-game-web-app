import { Box, Button, TextField, Typography } from "@mui/material";
import React from "react";
import { guardMessage, GuardRole, Role } from "../../../Utils";

export interface IProps {
    role: Role;
    guardRole: GuardRole;
}

export const ProfilePage = ({role, guardRole}: IProps) => {
    return(
        <Box>
            {
                role === Role.player && (
                    <Box>
                        <TextField>

                        </TextField>
                        <TextField>
                            
                        </TextField>
                        <TextField>
                            
                        </TextField>
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
                            {guardMessage(guardRole)}
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

        </Box>
    )
}