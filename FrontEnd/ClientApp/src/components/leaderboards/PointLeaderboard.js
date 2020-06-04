import 'rc-collapse/assets/index.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import React, { Component } from 'react';
import Collapse, { Panel } from 'rc-collapse';
import {Container, Row, Col } from 'react-bootstrap'
import i18n from "../../i18n";

export class PointLeaderboard extends Component {
  static displayName = PointLeaderboard.name;

  constructor(props) {
    super(props);
    this.state = 
    { 
      series: [], 
      loading: true,
      selectedPlayers: [] 
    };
  }

  componentDidMount() {
    this.populateResultData();
  }

  handlePlayerSelected(name) {
    var players = this.state.selectedPlayers;
    if(players.includes(name))
      players.splice(players.indexOf(name), 1);//remove
    else
    players.push(name);

    this.setState({ selectedPlayer: name });
    this.setState({ selectedPlayers: players });
  }

  renderSeriesTable(series) {
    return (<div>
      <Collapse
        accordion={false}>
        {this.getItems(series)}
      </Collapse>
    </div>
    );
  }

  getItems(series) {
    const items = [];
    
    for (let i = 0, len = series.length; i < len; i++) {
      const key = i + 1;
      var data = this.getPlayersData(series[i]);
      items.push(
        <Panel header={`${series[i].serieName}`} key={key}>
          <Container>
            <Row>
               <Col sm={12} lg={12}>
                   <div>{i18n.t('series_basednumrounds', {cnt: series[i].roundsToCount } )}</div>
               </Col>
            </Row>
            <Row>
              <Col sm={12} lg={12}>
                <table className='table' aria-labelledby="tabelLabel">
                  <thead>
                    <tr>
                      <th>{i18n.t('column_place')}</th>
                      <th>{i18n.t('column_name')}</th>
                      <th>{i18n.t('column_totalpoints')}</th>
                      <th className="d-none d-lg-table-cell">{i18n.t('column_avgpoints')}</th>
                      <th className="d-none d-md-table-cell">{i18n.t('column_avghcpscore')}</th> 
                      <th className="d-none d-lg-table-cell">{i18n.t('column_totalhcpscore')}</th>
                      <th className="d-none d-sm-table-cell">{i18n.t('column_maxpoints')}</th>
                      <th className="d-none d-sm-table-cell">{i18n.t('column_minpoints')}</th>
                      <th className="d-none d-lg-table-cell">{i18n.t('column_rounds')}</th>
                    </tr>
                  </thead>
                  <tbody>
                    {data.map(obj =>
                      this.getRow(obj)
                    )}
                  </tbody>
                </table>
              </Col>
            </Row>
        </Container>
        </Panel>
      );
    }
    return items;
  }

  getRow(obj)
  {
    if(obj.isDetails)
    {
      return (
        <tr>
          <td colSpan={9} style={{textAlign: 'center'}}>{obj.player.topResults.map(x => x.points).join(' - ')}</td>
        </tr>
      );
    }

    return (
      <tr key={obj.player.fullName} onClick={() => this.handlePlayerSelected(obj.player.fullName)}>
        <td>{obj.player.place}</td>
        <td>{obj.player.fullName}</td>
        <td>{obj.player.totalPoints}</td>
        <td className="d-none d-lg-table-cell">{obj.player.avgPoints}</td>
        <td className="d-none d-md-table-cell">{obj.player.avgHcpScore}</td>
        <td className="d-none d-lg-table-cell">{obj.player.totalHcpScore}</td>
        <td className="d-none d-sm-table-cell">{obj.player.topResults[0].points}</td>
        <td className="d-none d-sm-table-cell">{obj.player.topResults[obj.player.topResults.length - 1].points}</td>
        <td className="d-none d-lg-table-cell">{obj.player.numberOfRounds}</td>
      </tr>
    );
  }

  getPlayersData(serie)
  {
    var result = [];
    serie.placements.map(player =>
      {
        result.push(
        {
          isDetails: false,
          player: player
        });

        if(this.state.selectedPlayers.includes(player.fullName))
        {
          result.push(
            {
              isDetails: true,
              player: player
            });
        }
      }
    );

    return result;
  }

  render() {
    let contents = this.state.loading
      ? <p><em>{i18n.t('common_loading')}</em></p>
      : this.renderSeriesTable(this.state.series);
     
    return (
      <div>
        <h1 id="tabelLabel">{i18n.t('leaderboard_points_header')}</h1>
        <p>{i18n.t('series_description')}</p>
        {contents}
      </div>
    );
  }

  async populateResultData() {
    const response = await fetch('http://orbitibro.ddns.net:8088/api/Series/hcpLeaderbords');
    const data = await response.json();
    this.setState({ series: data, loading: false });
  }
}
