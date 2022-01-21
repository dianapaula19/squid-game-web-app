import React, { useState } from "react"
import { Button, TextField } from "@mui/material"

interface IProps {
    isAdmin: boolean;
}

interface IFormInput {
    email: string;
    socialId: string;
    password: string;
}

export const LoginForm = ({
    isAdmin
}: IProps) => {

    const [formData, setFormData] = useState<IFormInput>({
        email: '',
        socialId: '',
        password: '',
    })

    const handleChange = (event: React.ChangeEvent<{ value: unknown }>) => {
        const target = event.target as HTMLInputElement
        setFormData({...formData, [target.name]: target.value})
    }

    const onSubmit = () => {
        console.log(formData)
    }
    
    return(
        <div>
            {isAdmin ? (
                <TextField
                id="login-form-email" 
                name="email"
                label="Email" 
                variant="outlined"
                onChange={handleChange}
                />
            ) : (
                <TextField
                id="login-form-social-id" 
                name="socialId"
                label="Social Identification No." 
                variant="outlined" 
                onChange={handleChange}
                />
            )}
            <br/>
            <TextField
                id="login-form-password" 
                name="password" 
                label="Password" 
                variant="outlined" 
                type="password" 
                onChange={handleChange}
            />
            <br/>
            <Button onClick={onSubmit}>Sign In</Button>
        </div>
    )
}
