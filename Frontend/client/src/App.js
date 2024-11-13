import './App.css';
import {Route, Switch} from 'react-router-dom';
import { ThemeProvider } from '@livechat/ui-kit'
import { BackTop } from 'antd';


const displayNone={
  display: 'none',
}
const displayBlock={
  display: 'inline-block',
}
const defaultTheme = {
  FixedWrapperMaximized: {
    css: {
        boxShadow: '0 0 1em rgba(0, 0, 0, 0.1)',
    }
  }
}


function App() {
  return (
    <>
      <div style={window.location.pathname.includes('ResetPassword') ? displayNone : null}>
        <ThemeProvider theme={defaultTheme}>
        <Switch>
          
          </Switch>
          </ThemeProvider>
      </div>
    </>
  );
}

export default App;
