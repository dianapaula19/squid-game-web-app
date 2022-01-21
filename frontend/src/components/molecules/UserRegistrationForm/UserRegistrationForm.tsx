import React, { useState } from "react"
import { Button, MenuItem, Select, TextField, InputLabel } from "@mui/material"

interface IProps {
    isPlayer: boolean;
}

interface IFormInput {
    socialId: string;
    password: string;
    country: string;
    firstName: string;
    lastName: string;
    gender: string;
}

export const UserRegistrationForm = ({
    isPlayer
}: IProps) => {

    const [formData, setFormData] = useState<IFormInput>({
        socialId: '',
        password: '',
        country: '',
        firstName: '',
        lastName: '',
        gender: '',
    })

    const handleChange = (event: React.ChangeEvent<{ value: unknown }>) => {
        const target = event.target as HTMLInputElement;
        setFormData({...formData, [target.name]: target.value});
    }

    const onSubmit = () => {
        console.log(formData)
    }
    
    return(
        <div>
            <TextField
                id="login-form-social-id" 
                name="socialId"
                label="Social Identification No." 
                variant="outlined" 
                onChange={handleChange}
            />
            <br/>
            <InputLabel id="user-registration-form-country-label">Country</InputLabel>
            <Select
                labelId="user-registration-form-country-label"
                id="user-registration-form-country"
                value={formData.country}
                label="Country"
                onChange={event => setFormData({...formData, 'country': event.target.value as string})}
            >
                <MenuItem value={'Romania'}>Romania</MenuItem>
                <MenuItem value={'South Korea'}>South Korea</MenuItem>
                <MenuItem value={'USA'}>USA</MenuItem>
            </Select>
            <br/>
            {isPlayer && (
                <div>
                    <TextField
                        id="user-registration-form-first-name" 
                        name="firstName"
                        label="First Name" 
                        variant="outlined"
                        onChange={handleChange}
                    />
                    <br/>
                    <TextField
                        id="user-registration-form-last-name" 
                        name="lastName"
                        label="Last Name" 
                        variant="outlined"
                        onChange={handleChange}
                    />
                    <br/>
                    <InputLabel id="user-registration-form-gender-label">Gender</InputLabel>
                    <Select
                        labelId="user-registration-form-gender-label"
                        id="user-registration-form-gender"
                        value={formData.gender}
                        label="Gender"
                        onChange={event => setFormData({...formData, 'gender': event.target.value as string})}
                    >
                        <MenuItem value={'Female'}>Female</MenuItem>
                        <MenuItem value={'Male'}>Male</MenuItem>
                    </Select>
                </div>
                ) 
            }
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
