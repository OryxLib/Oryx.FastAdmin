import React from 'react';
import PropTypes from 'prop-types';
import { intlShape, injectIntl } from 'react-intl';
import bindAll from 'lodash.bindall';
import { connect } from 'react-redux';

import { setProjectUnchanged } from '../reducers/project-changed';

import {
    LoadingStates,
    getIsCreatingNew,
    getIsFetchingWithId,
    getIsLoading,
    getIsShowingProject,
    onFetchedProjectData,
    projectError,
    setProjectId
} from '../reducers/project-state';
import {
    activateTab,
    BLOCKS_TAB_INDEX
} from '../reducers/editor-tab';

import log from './log';
import storage from './storage';
const parser = require('scratch-parser');
const ProjectInfo = require('./project-info');
/* Higher Order Component to provide behavior for loading projects by id. If
 * there's no id, the default project is loaded.
 * @param {React.Component} WrappedComponent component to receive projectData prop
 * @returns {React.Component} component with project loading behavior
 */
const ProjectFetcherHOC = function (WrappedComponent) {
    class ProjectFetcherComponent extends React.Component {
        constructor(props) {
            super(props);
            bindAll(this, [
                'fetchProject'
            ]);
            storage.setProjectHost(props.projectHost);
            storage.setAssetHost(props.assetHost);
            storage.setTranslatorFunction(props.intl.formatMessage);
            // props.projectId might be unset, in which case we use our default;
            // or it may be set by an even higher HOC, and passed to us.
            // Either way, we now know what the initial projectId should be, so
            // set it in the redux store.
            if (
                props.projectId !== '' &&
                props.projectId !== null &&
                typeof props.projectId !== 'undefined'
            ) {
                console.log('set project')
                this.props.setProjectId(props.projectId.toString());
            }
        }
        componentDidUpdate(prevProps) {
            console.log('project fetcher hoc')
            if (prevProps.projectHost !== this.props.projectHost) {
                console.log('project fetcher hoc 1')
                storage.setProjectHost(this.props.projectHost);
            }
            if (prevProps.assetHost !== this.props.assetHost) {
                console.log('project fetcher hoc 2')

                storage.setAssetHost(this.props.assetHost);
            }
            if (this.props.isFetchingWithId && !prevProps.isFetchingWithId) {
                console.log('project fetcher hoc 3')

                this.fetchProject(this.props.reduxProjectId, this.props.loadingState);
            }
            if (this.props.isShowingProject && !prevProps.isShowingProject) {
                console.log('project fetcher hoc 4')

                this.props.onProjectUnchanged();
            }
            if (this.props.isShowingProject && (prevProps.isLoadingProject || prevProps.isCreatingNew)) {
                console.log('project fetcher hoc 5')

                this.props.onActivateTab(BLOCKS_TAB_INDEX);
            }
        }
        fetchProject(projectId, loadingState) {
            return storage
                .load(storage.AssetType.Project, projectId, storage.DataFormat.JSON)
                .then(projectAsset => {
                    console.log('fetch project')
                    console.log(projectAsset)
                    if (projectAsset) {
                        console.log('emit project')
                        this.props.onFetchedProjectData(projectAsset.data, loadingState);

                        console.log('%cparser project', "color:#f00;")
                        parser(projectAsset.data, false, (err, projectData) => {
                            if (err) {
                                log.error(`Unhandled project parsing error: ${err}`);
                                return;
                            }
                            const newState = {
                                modInfo: {} // Filled in below
                            };

                            const helpers = ProjectInfo[projectData[0].projectVersion];
                            if (!helpers) return; // sb1 not handled
                            newState.extensions = Array.from(helpers.extensions(projectData[0]));
                            newState.modInfo.scriptCount = helpers.scriptCount(projectData[0]);
                            newState.modInfo.spriteCount = helpers.spriteCount(projectData[0]);
                            const hasCloudData = helpers.cloudData(projectData[0]);
                            if (hasCloudData) {
                                if (this.props.isLoggedIn) {
                                    // show cloud variables log link if logged in
                                    newState.extensions.push({
                                        action: {
                                            l10nId: 'project.cloudDataLink',
                                            uri: `/cloudmonitor/${projectId}/`
                                        },
                                        icon: 'clouddata.svg',
                                        l10nId: 'project.cloudVariables',
                                        linked: true
                                    });
                                } else {
                                    newState.extensions.push({
                                        icon: 'clouddata.svg',
                                        l10nId: 'project.cloudVariables'
                                    });
                                }
                            }

                            // if (showAlerts) {
                            //     // Check for username block only if user is logged in
                            //     if (this.props.isLoggedIn) {
                            //         newState.showUsernameBlockAlert = helpers.usernameBlock(projectData[0]);
                            //     } else { // Check for cloud vars only if user is logged out
                            //         newState.showCloudDataAlert = hasCloudData;
                            //     }
                            // }
                            this.setState(newState);
                        });
                    } else {
                        // Treat failure to load as an error
                        // Throw to be caught by catch later on
                        throw new Error('Could not find project');
                    }
                })
                .catch(err => {
                    this.props.onError(err);
                    log.error(err);
                });
        }
        render() {
            const {
                /* eslint-disable no-unused-vars */
                assetHost,
                intl,
                isLoadingProject: isLoadingProjectProp,
                loadingState,
                onActivateTab,
                onError: onErrorProp,
                onFetchedProjectData: onFetchedProjectDataProp,
                onProjectUnchanged,
                projectHost,
                projectId,
                reduxProjectId,
                setProjectId: setProjectIdProp,
                /* eslint-enable no-unused-vars */
                isFetchingWithId: isFetchingWithIdProp,
                ...componentProps
            } = this.props;
            return (
                <WrappedComponent
                    fetchingProject={isFetchingWithIdProp}
                    {...componentProps}
                />
            );
        }
    }
    ProjectFetcherComponent.propTypes = {
        assetHost: PropTypes.string,
        canSave: PropTypes.bool,
        intl: intlShape.isRequired,
        isFetchingWithId: PropTypes.bool,
        isLoadingProject: PropTypes.bool,
        loadingState: PropTypes.oneOf(LoadingStates),
        onActivateTab: PropTypes.func,
        onError: PropTypes.func,
        onFetchedProjectData: PropTypes.func,
        onProjectUnchanged: PropTypes.func,
        projectHost: PropTypes.string,
        projectId: PropTypes.oneOfType([PropTypes.string, PropTypes.number]),
        reduxProjectId: PropTypes.oneOfType([PropTypes.string, PropTypes.number]),
        setProjectId: PropTypes.func
    };
    ProjectFetcherComponent.defaultProps = {
        assetHost: '//mioto.milbit.com',//'//mioto.milbit.com',,//'https://assets.scratch.mit.edu',// /Scratch/Asset
        projectHost: '/Scratch/Project' //'https://projects.scratch.mit.edu'
    };

    const mapStateToProps = state => ({
        isCreatingNew: getIsCreatingNew(state.scratchGui.projectState.loadingState),
        isFetchingWithId: getIsFetchingWithId(state.scratchGui.projectState.loadingState),
        isLoadingProject: getIsLoading(state.scratchGui.projectState.loadingState),
        isShowingProject: getIsShowingProject(state.scratchGui.projectState.loadingState),
        loadingState: state.scratchGui.projectState.loadingState,
        reduxProjectId: state.scratchGui.projectState.projectId
    });
    const mapDispatchToProps = dispatch => ({
        onActivateTab: tab => dispatch(activateTab(tab)),
        onError: error => dispatch(projectError(error)),
        onFetchedProjectData: (projectData, loadingState) =>
            dispatch(onFetchedProjectData(projectData, loadingState)),
        setProjectId: projectId => dispatch(setProjectId(projectId)),
        onProjectUnchanged: () => dispatch(setProjectUnchanged())
    });
    // Allow incoming props to override redux-provided props. Used to mock in tests.
    const mergeProps = (stateProps, dispatchProps, ownProps) => Object.assign(
        {}, stateProps, dispatchProps, ownProps
    );
    return injectIntl(connect(
        mapStateToProps,
        mapDispatchToProps,
        mergeProps
    )(ProjectFetcherComponent));
};

export {
    ProjectFetcherHOC as default
};
