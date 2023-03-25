import { useState } from 'react';
import TextField from '@mui/material/TextField';
import Button from '@mui/material/Button';

export default function Form({ fields, onSubmit }) {
  const [formData, setFormData] = useState({});

  const handleChange = (e) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value,
    });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    onSubmit(formData);
  };

  return (
    <form onSubmit={handleSubmit}>
      {fields.map((field) => (
        <TextField
          key={field.name}
          name={field.name}
          label={field.label}
          value={formData[field.name] || ''}
          onChange={handleChange}
          fullWidth
          margin="normal"
        />
      ))}
      <Button type="submit" variant="contained">
        Submit
      </Button>
    </form>
  );
}