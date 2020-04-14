/**
 * @flow
 */
import {
    connect
} from 'react-redux'; 
import { loadSchema } from '../actions/ActionCreators';
import { Schema } from '../components/Schema';

const mapStateToProps = (state) => ({
    store : state
});

const mapDispatchToProps = (dispatch) => ({
    loadSchema: (shema) => {
        dispatch(loadSchema(shema))
    }
});

export default connect(mapStateToProps, mapDispatchToProps)(Schema);