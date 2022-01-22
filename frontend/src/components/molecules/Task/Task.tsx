import React from "react";
import { CardActions, CardContent, Typography, Card, Button, withStyles } from "@mui/material";
import { purple, red } from "@mui/material/colors";

interface IProps {
    id: string;
    title: string;
    deadline: string;
}

export const Task = ({id, title, deadline}: IProps) => {

    const completeTask = () => {

    }

    return(
        <Card>
        <CardContent>
            <Typography variant="h4" gutterBottom>
            {title}
            </Typography>
        </CardContent>
        <CardActions>
            <Button
                size="small" 
                variant="outlined"
                onClick={completeTask}>
                Complete Task
            </Button>
        </CardActions>
    </Card>
    )
}