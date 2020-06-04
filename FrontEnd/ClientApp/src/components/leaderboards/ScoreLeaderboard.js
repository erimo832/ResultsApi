import 'rc-collapse/assets/index.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import React, { Component } from 'react';
import Collapse, { Panel } from 'rc-collapse';
import {Container, Row, Col } from 'react-bootstrap'
import i18n from "../../i18n";

export class ScoreLeaderboard extends Component {
  static displayName = ScoreLeaderboard.name;

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
        accordion={false}
      >
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
                <table className='table' aria-labelledby="tabelLabel"> {/* table-striped */}
                  <thead>
                    <tr>
                      <th>{i18n.t('column_place')}</th>
                      <th>{i18n.t('column_name')}</th>
                      <th>{i18n.t('column_avgscore')}</th>
                      <th className="d-none d-lg-table-cell">{i18n.t('column_totalscore')}</th>
                      <th className="d-none d-sm-table-cell">{i18n.t('column_minthrows')}</th>
                      <th className="d-none d-sm-table-cell">{i18n.t('column_maxthrows')}</th>
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
          <td colSpan={9} style={{textAlign: 'center'}}>{obj.player.topResults.map(x => x.score).join(' - ')}</td>          
        </tr>
      );
    }

    return (
      <tr key={obj.player.fullName} onClick={() => this.handlePlayerSelected(obj.player.fullName)}>
        <td>{obj.player.place}</td>
        <td>{obj.player.fullName}</td>
        <td>{obj.player.avgScore}</td>
        <td className="d-none d-lg-table-cell">{obj.player.totalScore}</td>
        <td className="d-none d-sm-table-cell">{obj.player.topResults[0].score}</td>
        <td className="d-none d-sm-table-cell">{obj.player.topResults[obj.player.topResults.length - 1].score}</td>
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
        <h1 id="tabelLabel">{i18n.t('leaderboard_score_header')}</h1>
        <p>{i18n.t('series_description')}</p>
        {contents}
      </div>
    );
  }

  async populateResultData() {
    const response = await fetch('http://orbitibro.ddns.net:8088/api/Series/scoreLeaderbords');
    const data = await response.json();
    this.setState({ series: data, loading: false });
  }
}
