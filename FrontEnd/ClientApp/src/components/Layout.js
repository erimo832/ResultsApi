import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';
import { I18nextProvider } from 'react-i18next'

import i18n from '../i18n'

//export default class Layout extends Component {
export class Layout extends Component {
  //static displayName = Layout.name;

  render () {    
    const { t } = this.props;

    return (
      <div>
        <NavMenu t={t} />
        <Container>
          <I18nextProvider i18n={i18n}>
            {this.props.children}
          </I18nextProvider>
        </Container>
      </div>
    ); 
  }

  /*
  render () {    
    const { t } = this.props;

    return (
      <div>
        <NavMenu t={t} />
        <Container>
            {this.props.children}          
        </Container>
      </div>
    ); 
  }*/
};