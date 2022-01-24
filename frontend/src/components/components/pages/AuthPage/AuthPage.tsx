import { Box } from "@mui/system";
import React from "react";
import Background from "../../../../images/background.jpg"
import { LoginForm } from "../../molecules/forms/LoginForm/LoginForm";
import { RegistrationForm } from "../../molecules/forms/RegistrationForm/RegistrationForm";

export enum AuthOption {
    login = "Login",
    register = "Register"
}

export interface IProps {
    option: AuthOption
}

export const AuthPage = ({option}: IProps) => {
    return(
        <Box sx={{
            backgroundImage: `url(${Background})`,
            backgroundBlendMode: 'lighten',
            backgroundColor: 'rgba(255,255,255,0.7)',
            width: '100%',
            height: "100vh",
            backgroundRepeat: "no-repeat",
            backgroundSize: "cover",
            display: 'flex',
            alignContent: 'center',
            justifyContent: 'end',
            alignSelf: 'center'
        }}>
            {
                option === AuthOption.login ? (
                    <LoginForm />        
                ) : (
                    <RegistrationForm />
                )
            }
        </Box>
    )
}