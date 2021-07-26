import React from "react";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import { fetchShipments } from "../Actions/ShipmentAction";
import ShipmentListItem from "../Components/ShipmentListItem";
class Shipments extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            lastId: null,
            shipments: []
        };
        this.more = this.more.bind(this)
    }

    componentDidMount() {
        this.props.fetchShipments();
    }
    
    more() {
        this.props.fetchShipments(this.state.lastId);
    }

    componentDidUpdate(oldProps) {
        if (!this.props.shipmentFetching && oldProps.shipmentFetching && this.props.shipmentFetched) {
            this.setState({
                shipments: [...this.state.shipments, ...this.props.shipment],
                lastId: this.props.shipment.length < 10 ? null : this.props.shipment[9].id
            });
        }
    }

    render() {
        return (
            <div className="container">
                <h1>Shipments</h1>
                {this.props.shipmentError &&
                    <div className="error">{this.props.shipmentError}</div>
                }
                {this.props.finishError &&
                    <div className="error">{this.props.finishError}</div>
                }
                <ShipmentListItem data={{
                    airport: 1,
                    status: 1,
                    shipmentNumber: "",
                    flightNumber: "",
                    flightDate: (new Date()).toISOString()}}/>
                {this.props.shipmentFetched && this.state.shipments.map((data) =>
                    <ShipmentListItem key={data.id} data={data} />
                )}
                {this.state.lastId && 
                    <button onClick={this.more}>Load more</button>
                }
            </div>
        );
    }
}
function mapStateToProps(state) {
    return {
        shipment: state.shipment.data,
        shipmentFetching: state.shipment.fetching,
        shipmentFetched: state.shipment.fetched,
        shipmentError: state.shipment.error,

        finishError: state.shipmentFinish.error,
        shipmentError: state.shipmentPost.error,
    };
}

function matchDispatchToProps(dispatch) {
    return bindActionCreators({
        fetchShipments
    }, dispatch);
}

export default connect(mapStateToProps, matchDispatchToProps)(Shipments);