import React from 'react';
import { Provider } from 'react-redux';
import store from './store';
import Schema from '../src/components/Schema';
import './styles/bootstrap.css';
import './styles/main.css';

const App = () => (
    <Provider store={ store }>
        <Schema store ={ store }/>
    </Provider>
);

export default App;
