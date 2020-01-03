import 'rc-collapse/assets/index.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import React, { Component } from 'react';
import Collapse, { Panel } from 'rc-collapse';
import i18n from "../i18n";

export class Rounds extends Component {
  static displayName = Rounds.name;

  constructor(props) {
    super(props);
    this.state = { rounds: [], loading: true };
  }

  componentDidMount() {
    this.populateResultData();
  }

  static renderRoundsTable(rounds) {
    return (<div>           
      <Collapse
        accordion={false}
      >
        {this.getItems(rounds)}
      </Collapse>
    </div>
    );
  }

  static getItems(rounds) {
    const items = [];
    for (let i = 0, len = rounds.length; i < len; i++) {
      const key = i + 1;
      items.push(
        <Panel header={`${rounds[i].eventName}`} key={key}>
          <table className='table table-striped' aria-labelledby="tabelLabel">
          <thead>
            <tr>
              <th>{i18n.t('column_place')}</th>
              <th>{i18n.t('column_name')}</th>              
              <th>{i18n.t('column_points')}</th>              
              <th className="d-none d-sm-table-cell">{i18n.t('column_score')}</th>
              <th className="d-none d-sm-table-cell">{i18n.t('column_hcp')}</th>
              <th className="d-none d-sm-table-cell">{i18n.t('column_hcpscore')}</th>
            </tr>
          </thead>
          <tbody>
            {rounds[i].results.map(player =>
              <tr key={player.fullName}>
                  <td>{player.place}</td>
                  <td>{player.fullName}</td>                  
                  <td>{player.points}</td>
                  <td className="d-none d-sm-table-cell">{player.score}</td>
                  <td className="d-none d-sm-table-cell">{player.hcp}</td>                  
                  <td className="d-none d-sm-table-cell">{player.hcpScore}</td>
              </tr>
            )}
          </tbody>
        </table>
        </Panel>
      );
    }
    return items;
  }

  render() {
    let contents = this.state.loading
      ? <p><em>{i18n.t('common_loading')}</em></p>
      : Rounds.renderRoundsTable(this.state.rounds);
     
    return (
      <div>
        <h1 id="tabelLabel">{i18n.t('rounds_header')}</h1>
        <p>{i18n.t('rounds_description')}</p>
        {contents}
      </div>
    );
  }

  async populateResultData() {
    const response = await fetch('http://ceptor.myftp.org:8088/api/Rounds');
    const data = await response.json();
    this.setState({ rounds: data, loading: false });
  }
}
