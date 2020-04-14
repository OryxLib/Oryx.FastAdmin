import * as types from '../actions/constants';

const initialState = window.localStorage.getItem('schema');

export default (state = initialState, action) => {
    switch (action.type) {
        case types.LOAD_SCHEMA:
            return action.schema;
        default:
            return state;
    }
};