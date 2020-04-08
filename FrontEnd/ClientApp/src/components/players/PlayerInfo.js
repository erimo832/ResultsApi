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
            <table className='table table-striped table-condensed' aria-labelledby="tabelLabel">
            <thead>
                <tr>
                    <th>nt_EventName</th>
                    <th>nt_RoundDate</th>
                    <th>nt_Score</th>
                </tr>
            </thead>
            <tbody>
                {info[0].rounds.map(rounds =>
                <tr key={rounds.eventName}>
                    <td>{rounds.eventName}</td>
                    <td>{rounds.roundTime.substring(0,10)}</td>
                    <td>{rounds.score}</td>
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