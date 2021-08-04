import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Hcp } from './components/Hcp';
import { Rounds } from './components/Rounds';
import { PointLeaderboard } from './components/leaderboards/PointLeaderboard';
import { ScoreLeaderboard} from './components/leaderboards/ScoreLeaderboard';
import { CtpLeaderboard} from './components/leaderboards/CtpLeaderboard';
import './custom.css';
import { Players } from './components/players/Players';
import { PlayerInfo } from './components/players/PlayerInfo';

export default class App extends Component {
  static displayName = App.name;

  componentDidMount(){
    document.title = process.env.REACT_APP_WINDOW_TITLETEXT;
  }
  
  render () {    
    const { t } = this.props;
    return (
      <Layout t={t}>
        <Route exact path='/' component={Home} />
        <Route path='/hcp/' component={Hcp} />
        <Route path='/rounds' component={Rounds} />
        <Route path='/players' component={Players} />
        <Route path='/playerinfo' component={PlayerInfo} />
        <Route path='/leaderboards/point' component={PointLeaderboard} />
        <Route path='/leaderboards/score' component={ScoreLeaderboard} />
        <Route path='/leaderboards/ctp' component={CtpLeaderboard} />
      </Layout>
    );
  }
}