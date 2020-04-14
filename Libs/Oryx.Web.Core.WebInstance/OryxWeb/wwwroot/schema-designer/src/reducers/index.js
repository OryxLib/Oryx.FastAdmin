import { combineReducers } from 'redux';
import databaseReducer from './databaseReducer';
import uiReducer from './uiReducer';
import tableReducer from './tableReducer';
import columnReducer from './columnReducer';
import relationReducer from './relationReducer';
import initReducer from './initReducer' 
import * as type from '../actions/constants';
import * as API  from  '../Api/index';

export default (state, action) => {  
    state = appReducer(state,action)
    switch (action.type){
        case type.SAVE_DB_NAME:
        case type.SAVE_TABLE:
        case type.UPDATE_TABLE:
        case type.REMOVE_TABLE:
        case type.SAVE_COLUMN:
        case type.REMOVE_COLUMN: 
        case type.UPDATE_COLUMN: 
        case type.SAVE_FOREIGN_KEY_RELATION:
        case type.UPDATE_FOREIGN_KEY_RELATION: 
            // var _state = JSON.parse( JSON.stringify( store.getState()))
             console.log('Save Operate', "Upload Data")
            state.ui.column.showModal = false
            state.ui.table.showModal = false
            state.ui.database.showModal = false
            API.saveSchema(state) 
            break;
        case type.UpdateDB:
            API.updateSchema()
            break; 
    }
    if (action.type==type.LOAD_SCHEMA){
       return initReducer(state,action)
    }
    
    return state;
}

const appReducer = combineReducers({
    database: databaseReducer,
    ui: uiReducer,
    tables: tableReducer,
    columns: columnReducer,
    relations: relationReducer,
    initState: initReducer 
});