import React from "react";
import { withRouter, Switch, Route } from 'react-router-dom';

import Shipments from "./Pages/Shipments";
import Bags from "./Pages/Bags";
import Parcels from "./Pages/Parcels";

import {
    ShipmentsPath,
    BagsPath,
    ParcelsPath
} from "./Const/Routers";

class Layout extends React.Component {
    render() {
        return (
            <Switch>
                <Route path={ShipmentsPath} exact component={Shipments} />
                <Route path={BagsPath + ":shipmentId"} exact component={Bags} />
                <Route path={ParcelsPath + ":bagId"} exact component={Parcels} />
                <Route path="*" component={Shipments} />
            </Switch >
        );
    }
}

export default withRouter(Layout);
