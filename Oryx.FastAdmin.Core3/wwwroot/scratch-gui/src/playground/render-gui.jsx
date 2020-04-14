import React from 'react';
import ReactDOM from 'react-dom';
import { compose } from 'redux';

import AppStateHOC from '../lib/app-state-hoc.jsx'; 1452
import GUI from '../containers/gui.jsx';
import HashParserHOC from '../lib/hash-parser-hoc.jsx';
import TitledHOC from '../lib/titled-hoc.jsx';
import log from '../lib/log.js';
import xhr from 'xhr';

const onClickLogo = () => {
    window.location = 'https://scratch.pagetechs.com';
};

const handleTelemetryModalCancel = () => {
    log('User canceled telemetry modal');
};

const handleTelemetryModalOptIn = () => {
    log('User opted into telemetry');
};

const handleTelemetryModalOptOut = () => {
    log('User opted out of telemetry');
};

const handleSave = () => {
    log('Handle Save !');
};

const handleThumbnail = (id, data, blob) => {
    console.log('handleThumbnail')
    console.log(id)
    console.log(data)
    var fileName = 'thumb/' + id + '.jpg';
    xhr({ url: "/QiniuFile/TokenByKey?fileName=" + fileName }, (err, res) => {
        var token = res.body
        var formData = new FormData();
        formData.append('file', blob)
        formData.append('token', token)
        formData.append('key', fileName)
        xhr({ url: '//upload-z2.qiniup.com', data: formData, method: 'post' }, (err, res) => {
            console.log(res)
            'mioto.milbit.com/' + res.body.key
        })
    })
}

const handleEdit = (res) => {
    console.log('handleEdit');
    console.log(res);
    xhr({}, (err, res) => {

    })
}

/*
 * Render the GUI playground. This is a separate function because importing anything
 * that instantiates the VM causes unsupported browsers to crash
 * {object} appTarget - the DOM element to render to
 */
export default appTarget => {
    GUI.setAppElement(appTarget);

    // note that redux's 'compose' function is just being used as a general utility to make
    // the hierarchy of HOC constructor calls clearer here; it has nothing to do with redux's
    // ability to compose reducers.
    const WrappedGui = compose(
        AppStateHOC,
        HashParserHOC,
        TitledHOC
    )(GUI);

    console.log("WrappedGui")
    console.log(WrappedGui)
    // TODO a hack for testing the backpack, allow backpack host to be set by url param
    const backpackHostMatches = window.location.href.match(/[?&]backpack_host=([^&]*)&?/);
    const backpackHost = backpackHostMatches ? backpackHostMatches[1] : null;

    const scratchDesktopMatches = window.location.href.match(/[?&]isScratchDesktop=([^&]+)/);
    let simulateScratchDesktop;
    if (scratchDesktopMatches) {
        try {
            // parse 'true' into `true`, 'false' into `false`, etc.
            simulateScratchDesktop = JSON.parse(scratchDesktopMatches[1]);
        } catch {
            // it's not JSON so just use the string
            // note that a typo like "falsy" will be treated as true
            simulateScratchDesktop = scratchDesktopMatches[1];
        }
    }

    if (process.env.NODE_ENV === 'production' && typeof window === 'object') {
        // Warn before navigating away
        window.onbeforeunload = () => true;
    }
    let routes = window.location.href.split('/');
    let projectId = routes[routes.length - 1];//routes.length >= 4 ? window.location.href.split('/')[4] : "0";
    projectId = projectId || 'd7502cee-0712-4ae2-bd8d-93b8eab0a694'
    console.log('projectId')
    console.log(projectId)
    let playModel = window.location.hash.toLowerCase().indexOf('play')>-1 || window.location.href.toLowerCase().indexOf('preview')>-1
    ReactDOM.render(
        // important: this is checking whether `simulateScratchDesktop` is truthy, not just defined!
        simulateScratchDesktop ?
            <WrappedGui
                isScratchDesktop
                showTelemetryModal
                canSave={true}
                onTelemetryModalCancel={handleTelemetryModalCancel}
                onTelemetryModalOptIn={handleTelemetryModalOptIn}
                onTelemetryModalOptOut={handleTelemetryModalOptOut}
                onClickSave={handleSave}
            /> :
            <WrappedGui
                basePath={"/"}
                backpackVisible
                showComingSoon={false}
                backpackHost={backpackHost}
                projectId={projectId}
                canSave={true}

                isPlayerOnly={playModel}
                onClickLogo={handleSave}
                onClickSave={handleSave}
                onUpdateProjectTitle={handleEdit}
                onUpdateProjectThumbnail={handleThumbnail}
            />,
        appTarget);
};
