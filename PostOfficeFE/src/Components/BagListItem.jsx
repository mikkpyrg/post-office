import React from "react";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import { postBag } from "../Actions/BagAction";
import { Link } from "react-router-dom";
import { ParcelsPath } from "../Const/Routers";
import DateTimePicker from 'react-datetime-picker';
import Select from 'react-select';

class BagListItem extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            data: props.data,
            showContent: false
        };
        this.onChange = this.onChange.bind(this);
        this.onChangeDropdown = this.onChangeDropdown.bind(this);
        this.toggleContent = this.toggleContent.bind(this)
        this.onPost = this.onPost.bind(this)
    }

    mapStatusToString(status) {
        switch(status) {
            case 1:
                return "Parcel"
            case 2:
                return "Letter"
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

    onChangeDropdown(option) {
        var data = {...this.state.data}
        data.bagType = option.value;
        this.setState({data})
    }

    onPost() {
        const data = this.state.data;
        let postData = data.bagType == 1
        ? {
            shipmentId: data.shipmentId,
            id: data.id,
            bagNumber: data.bagNumber,
            bagType: data.bagType
        }
        : this.state.data;
        if (postData.id !== null) {
            postData.bagType = null;
        }
        this.props.postBag(postData)
        .then(() => {
            location.reload();
        });
    }

    render() {
        const data = this.state.data;
        const bagTypes = [        
            { value: 1, label: 'Parcel' },
            { value: 2, label: 'Letter' }
        ];
        const currentValue = bagTypes.filter(o => o.value === data.bagType);
        return (
            <>
                <div className="list-item-header" onClick={this.toggleContent}>
                    <span>{data.id ? data.bagNumber : "Create New"}</span>
                    <span>{`${this.mapStatusToString(data.bagType)}: ${data.bagType === 1 ? data.parcelCount : data.countOfLetters}`}</span>
                </div>
                {this.state.showContent &&
                    <div className="list-item-content">
                        <span>Bag number</span>
                        <input type="text" name="bagNumber" value={data.bagNumber} onChange={this.onChange} />
                        <span>Type</span>
                        {data.id  
                            ? <span>{this.mapStatusToString(data.bagType)}</span>
                            : <Select value={currentValue} options={bagTypes} onChange={this.onChangeDropdown}/>
                        }
                        {data.bagType == 2 &&
                          <>
                            <span>Count of letters</span>
                            <input type="number" name="countOfLetters" min="1" value={data.countOfLetters} onChange={this.onChange} />
                            <span>Weight</span>
                            <input type="number" name="weight" min="0" value={data.weight} onChange={this.onChange} />
                            <span>Price</span>
                            <input type="number" name="price" min="0" value={data.price} onChange={this.onChange} />
                          </>
                        }
                        {data.id && data.bagType == 1 &&
                            <Link to={ParcelsPath + data.id}>View Parcels</Link>
                        }
                        <button onClick={this.onPost}>Update</button>


                    </div>
                }
            </>
        );
    }
}
function mapStateToProps(state) {
    return {
        bag: state.bagPost.data
    };
}

function matchDispatchToProps(dispatch) {
    return bindActionCreators({
        postBag
    }, dispatch);
}

export default connect(mapStateToProps, matchDispatchToProps)(BagListItem);