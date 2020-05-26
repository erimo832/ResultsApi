import 'bootstrap/dist/css/bootstrap.min.css';
import React, { Component } from 'react';
import { Nav, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import i18n from "../../i18n";

export class PlayerInfo extends Component {
  static displayName = PlayerInfo.name;

  constructor(props) {
    super(props);
    var name = "";
    if(props.location.playerName)
        name = props.location.playerName.player;

    this.state = { 
        playerInfo: [],         
        name: name,
        loading: true 
    };
  }

  componentDidMount() {
    this.populateResultData();
  }

//npm i react-apexcharts

  //Ugly substring to show date
  static renderPlayerInfoTable(info) {
    if(info.length == 0)
        return (<div>nt_No player</div>);

    return (
        <div>
            <div></div>
            <table className='table table-condensed table-sm' aria-labelledby="tabelLabel">
            <thead>
                <tr>                    
                    <th>nt_RoundDate</th>
                    <th>nt_Event name</th>
                    <th>nt_Score</th>
                    <th>nt_Hcp</th>
                    <th>nt_HcpScore</th>
                    <th>nt_Points</th>
                </tr>
            </thead>
            <tbody>
                {info.events.map(x =>
                <tr key={x.time} class={x.inHcpAvgCalculation ? 'avg' : (x.inHcpCalculation === true ? 'top' : 'none' ) }>
                    <td>{x.eventName}</td>
                    <td>{x.time.substring(0,10)}</td>                    
                    <td>{x.score}</td>
                    <td>{x.hcp}</td>
                    <td>{x.hcpScore}</td>
                    <td>{x.roundPoints}</td>
                </tr>
                )}
            </tbody>
            </table>
        </div>
    );
  }

  render() {    
    let contents = this.state.loading
      ? <p><em>{i18n.t('common_loading')}</em></p>
      : PlayerInfo.renderPlayerInfoTable(this.state.playerInfo);
    
    return (
      <div>
        <h1 id="tabelLabel">{this.state.name}</h1>
        <p>nt_Players info text</p>        
        {contents}
      </div>
    );
  }

  async populateResultData() {
    var data = [];
    if(this.state.name != "") {
        const response = await fetch('http://orbitibro.ddns.net:8088/api/Players/' + this.state.name);
        data = await response.json();
    }
    this.setState({ playerInfo: data, loading: false });
  }
}