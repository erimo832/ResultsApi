import 'bootstrap/dist/css/bootstrap.min.css';
import React, { Component } from 'react';
import i18n from "../i18n";

export class Hcp extends Component {
  static displayName = Hcp.name;

  constructor(props) {
    super(props);
    this.state = { hcp: [], loading: true };
  }

  componentDidMount() {
    this.populateResultData();
  }

  static renderHcpTable(hcpList) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>{i18n.t('column_name')}</th>
            <th>{i18n.t('column_hcp')}</th>
          </tr>
        </thead>
        <tbody>
          {hcpList.map(player =>
            <tr key={player.fullName}>
                <td>{player.fullName}</td>
                <td>{player.hcp}</td>
              
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {    
    let contents = this.state.loading
      ? <p><em>{i18n.t('common_loading')}</em></p>
      : Hcp.renderHcpTable(this.state.hcp);
     
    return (
      <div>
        <h1 id="tabelLabel">{i18n.t('hcp_header')}</h1>
        <p>{i18n.t('hcp_description_avgscore')}</p>
        <p>{i18n.t('hcp_description_hcp')}</p>
        {contents}
      </div>
    );
  }

  async populateResultData() {
    const response = await fetch('http://orbitibro.ddns.net:8088/api/Players/currentHcp');
    const data = await response.json();
    this.setState({ hcp: data, loading: false });
  }
}