import 'rc-collapse/assets/index.css';
import React, { Component } from 'react';

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
            <th>Name</th>
            <th>Hcp</th>
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
      ? <p><em>Loading...</em></p>
      : Hcp.renderHcpTable(this.state.hcp);
     
    return (
      <div>
        <h1 id="tabelLabel">Current Hcp</h1>
        <p>Some info how hcp is calculated</p>
        {contents}
      </div>
    );
  }

  async populateResultData() {
    const response = await fetch('http://ceptor.myftp.org:8088/api/Players/currentHcp');
    const data = await response.json();
    this.setState({ hcp: data, loading: false });
  }
}
