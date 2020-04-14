/**
 * @flow
 */
import React from 'react';
import DrawRelationLine from './DrawRelationLine';
import Header from '../containers/Header';
import DbModal from '../containers/Modals/DbModal';
import TableModal from '../containers/Modals/TableModal';
import ColumnModal from '../containers/Modals/ColumnModal';
import Tables from '../containers/Tables'; 
import * as type from '../actions/constants'
import loadSchema from '../actions/ActionCreators';
import * as API from '../Api';

const Schema = (props) => {
    props.store.subscribe(state=>{
        console.log('(state)', state)
    })
    console.log('Schema rendering'); // eslint-disable-line no-console
    API.loadSchema()
    .then(res=>{
        props.store.dispatch(loadSchema(JSON.parse( res.data.schema)))
    })
    
    return (
        <div className='container-fluid'>
            <Header />

            <Tables />

            <DbModal />

            <TableModal />

            <ColumnModal />

            <DrawRelationLine />
        </div>
    );
};

export default Schema;
