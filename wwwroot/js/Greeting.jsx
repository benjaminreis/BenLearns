class CommentBox extends React.Component {
  render() {
    return (
      <div className="commentBox">
        Welcome to the Volunteer App!  This is rendered within React.JS!
      </div>
    );
  }
}

ReactDOM.render(
  <CommentBox />,
  document.getElementById('content')
);
