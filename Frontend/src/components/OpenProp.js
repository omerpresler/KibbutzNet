import Dialog from '@mui/material/Dialog';
import { useState } from 'react';
import Center from './Center';

export default function OpenProp(prop) {


return (
    <div>
      {open && (
        <Center>
          {prop}
          <button onClick={handleClick}>
        {open ? 'Close' : 'Open'}
      </button>
        </Center>
      )}
    </div>
  );
}



