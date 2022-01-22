import React, { useState } from "react"
import { Button, TextField } from "@mui/material"
import DesktopDatePicker from '@mui/lab/DesktopDatePicker'

interface IFormInput {
    title: string;
    deadline: string;
}

export const CreateTaskForm = () => {

    const [formData, setFormData] = useState<IFormInput>({
        title: '',
        deadline: ''
    })

    const handleChange = (event: React.ChangeEvent<{ value: unknown }>) => {
        const target = event.target as HTMLInputElement
        setFormData({...formData, [target.name]: target.value})
    }

    const handleChangeDate = (date: string | null, name: string) => {
        setFormData({...formData, [name]: date})
    }

    const onSubmit = () => {
        console.log(formData)
    }
    
    return(
        <div>
            <TextField
                id="create-task-form-title" 
                name="title" 
                label="Title" 
                variant="outlined"  
                onChange={handleChange}
            />
            <br/>
            <DesktopDatePicker
                label="Date desktop"
                inputFormat="dd/mm/yyyy"
                value={formData.deadline}
                onChange={(date)=> handleChangeDate(date, 'deadline')}
                renderInput={(params) => <TextField {...params} />}
            >
                
            </DesktopDatePicker>
            <Button onClick={onSubmit}>Create task</Button>
        </div>
    )
}
