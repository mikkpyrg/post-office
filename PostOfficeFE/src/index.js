import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import { BrowserRouter as Router } from "react-router-dom";
import Store from "./Store";
import Layout from "./Layout";
import "./Styles/_layout.scss";
import Shipments from "./Pages/Shipments";
ReactDOM.render(
    <Provider store={Store}>
		<Router>
			<Layout />
		</Router>
	</Provider>,
  document.getElementById('root')
);
