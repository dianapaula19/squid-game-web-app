import { Button, LinearProgress, Typography } from "@mui/material";
import { makeStyles } from "@mui/styles";
import { Box, createTheme } from "@mui/system";
import React, { useEffect, useState } from "react";
import GameBackground from "../../../../images/game-background.png";

const SECOND = 1000;
const colors = {
    'RED': 'rgba(179,44,111,0.7)',
    'GREEN': 'rgba(98,186,198,0.7)',
    'WHITE': 'rgba(255,255,255,0.7)'
}

export const GamePage = () => {

    const [progress, setProgress] = React.useState(0);

    const [data, setData] = useState({
        intervals: 0,
        isRed: false,
        gameStarted: false,
        gameOver: false,
        won: null,       
    });

    const startGame = () => {
        setData({
            ...data,
            gameStarted: true
        })
    };

    useEffect(() => {
      if (data.gameStarted && !data.gameOver) {
          setTimeout(() => {
            setData({
                ...data,
                isRed: !data.isRed
            });
          }, 5 * SECOND);
      }
    });
    

    return(
        <Box 
            sx={{
                backgroundImage: `url(${GameBackground})`,
                backgroundBlendMode: 'lighten',
                width: '100%',
                backgroundRepeat: "no-repeat",
                backgroundSize: "cover",
                height: "100vh",
                display: 'flex',
                flexDirection: 'column',
                justifyContent: 'center',
                alignContent: 'center',
                backgroundColor: `${data.gameStarted ? (data.isRed ? colors["RED"] : colors["GREEN"]) : colors["WHITE"]}`
            }}
        >
            {
                !data.gameStarted && (
                    <>  
                        <Button 
                        onClick={startGame} 
                        variant="contained"
                        sx={{
                            backgroundColor: '#b32c6f',
                            alignSelf: 'center',
                            maxWidth: '20%',
                            minWidth: '20%',
                            minHeight: '10%',
                            '&:hover': {
                                backgroundColor: '#62bac6',
                            }
                        }}
                        >
                        Start
                        </Button>
                    </>
                )
            }
            {
                data.gameStarted && (
                    <LinearProgress 
                        variant="determinate"
                        value={progress} 
                    />
                )
            }
        </Box>
    )
}