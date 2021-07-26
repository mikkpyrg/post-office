import React from "react";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import { postShipment, finishShipment } from "../Actions/ShipmentAction";
import { Link } from "react-router-dom";
import { BagsPath } from "../Const/Routers";
import DateTimePicker from 'react-datetime-picker';
import Select from 'react-select';

class ShipmentListItem extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            data: props.data,
            showContent: false
        };
        this.onChange = this.onChange.bind(this);
        this.onDateTimeChange = this.onDateTimeChange.bind(this);
        this.onChangeDropdown = this.onChangeDropdown.bind(this);
        this.toggleContent = this.toggleContent.bind(this)
        this.onPost = this.onPost.bind(this)
        this.onFinish = this.onFinish.bind(this)
    }

    mapStatusToString(status) {
        switch(status) {
            case 1:
                return "In progress"
            case 2:
                return "Finished"
            default:
                return "New"
        }
    }
    
    toggleContent() {
        this.setState({showContent: !this.state.showContent})
    }

    onChange(e) {
        var data = {...this.state.data}
        data[e.target.name] = e.target.value;
        this.setState({data})
    }

    onDateTimeChange(date) {
        var data = {...this.state.data}
        data.flightDate = date ? date.toISOString(): null;
        this.setState({data})
    }

    onChangeDropdown(option) {
        var data = {...this.state.data}
        data.airport = option.value;
        this.setState({data})
    }

    onPost() {
        this.props.postShipment(this.state.data)
        .then(() => {
            location.reload();
        });
    }

    onFinish() {
        this.props.finishShipment(this.state.data.id)
        .then(() => {
            location.reload();
        });
    }

    render() {
        const data = this.state.data;
        const flightDate = new Date(data.flightDate);
        const airportOptions = [        
            { value: 1, label: 'TLL' },
            { value: 2, label: 'RIX' },
            { value: 3, label: 'HEL' }
        ];
        const currentValue = airportOptions.filter(o => o.value === data.airport);
        return (
            <>
                <div className="list-item-header" onClick={this.toggleContent}>
                    <span>{data.id ? data.shipmentNumber : "Create New"}</span>
                    <span>{this.mapStatusToString(data.status)}</span>
                </div>
                {this.state.showContent &&
                    <div className="list-item-content">
                        <span>Shipment number</span>
                        <input type="text" name="shipmentNumber" value={data.shipmentNumber} onChange={this.onChange} />
                        <span>Flight number</span>
                        <input type="text" name="flightNumber" value={data.flightNumber} onChange={this.onChange} />
                        <span>Flight date</span>
                        <DateTimePicker onChange={this.onDateTimeChange} value={flightDate} />
                        <span>Airport</span>
                        <Select value={currentValue} options={airportOptions} onChange={this.onChangeDropdown}/>
                        <Link to={BagsPath + data.id}>View bags</Link>
                        {data.status !== 2 &&
                            <>
                                <button onClick={this.onPost}>Update</button>
                                {data.id &&
                                    <button onClick={this.onFinish}>Finish</button>
                                }
                            </>
                        }

                    </div>
                }
            </>
        );
    }
}
function mapStateToProps(state) {
    return {
        shipment: state.shipmentPost.data,

        finish: state.shipmentFinish.data
    };
}

function matchDispatchToProps(dispatch) {
    return bindActionCreators({
        postShipment,
        finishShipment
    }, dispatch);
}

export default connect(mapStateToProps, matchDispatchToProps)(ShipmentListItem);