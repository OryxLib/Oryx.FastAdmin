import {
    applyMiddleware,
    compose,
    createStore
} from 'redux';
import {
    createLogger
} from 'redux-logger';
import Reducers from '../reducers';
import LocalStorageMiddleware from './LocalStorageMiddleware';
import {
    getInitialState
} from '../utils/helpers';
import * as API  from  '../Api/index';


const logger = createLogger();
const middleware = [LocalStorageMiddleware];

let extension = (next) => next;

if (process.env.NODE_ENV !== 'production') {
    middleware.push(logger);
    extension = window.devToolsExtension ? window.devToolsExtension() : extension;
}

const initialState = getInitialState();

const store = createStore(Reducers, initialState, compose(applyMiddleware(...middleware), extension));
store.subscribe(() => {
    // var _state = JSON.parse( JSON.stringify( store.getState()))
    // console.log('Save Operate', "Upload Data")
    // _state.ui.column.showModal = false
    // _state.ui.table.showModal = false
    // _state.ui.database.showModal = false
    // API.saveSchema(_state)
})

if (module.hot) {
    // Enable Webpack hot module replacement for reducers
    module.hot.accept('../reducers', () => {
        const nextRootReducer = require('../reducers/index').default; // eslint-disable-line global-require
        store.replaceReducer(nextRootReducer);
    });
}

export default store;