import { Grid } from '@mui/material'
import React from 'react'

export default function Center(props) {
    return (
        <Grid container
            direction="column"
            alignItems="center"
            justifyContent="space-evenly"

            sx={{ minHeight: '100vh' }}>
            <Grid item xs={4} sm={6} rowSpacing={1} >
                {props.children}
            </Grid>
        </Grid>
    )
}