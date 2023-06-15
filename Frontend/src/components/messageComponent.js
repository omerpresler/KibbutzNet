import { ListItem, ListItemText, makeStyles } from '@mui/material';

const useStyles = makeStyles({
  alignLeft: {
    textAlign: 'left',
  },
  alignRight: {
    textAlign: 'right',
  },
});

export default function MessageComponent({ messages }) {
  const classes = useStyles();

  return (
    messages.map((message, index) => (
      <ListItem key={index}>
        <ListItemText
          classes={{
            primary: message.FromMe ? classes.alignLeft : classes.alignRight,
            secondary: message.FromMe ? classes.alignLeft : classes.alignRight,
          }}
          primary={message.FromMe ? 'אני' : 'הם'}
          secondary={message.message}
        />
      </ListItem>
    ))
  );
}