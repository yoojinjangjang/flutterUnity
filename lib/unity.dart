import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_unity_widget/flutter_unity_widget.dart';
import 'package:flutterunity/main.dart';

void main() {

  runApp(MaterialApp(
      home: UnityDemoScreen()
  ));

}

class UnityDemoScreen extends StatefulWidget {

  UnityDemoScreen({Key? key}) : super(key: key);

  @override
  _UnityDemoScreenState createState() => _UnityDemoScreenState();


}

class _UnityDemoScreenState extends State<UnityDemoScreen>{
  static final GlobalKey<ScaffoldState> _scaffoldKey =
  GlobalKey<ScaffoldState>();
  late UnityWidgetController _unityWidgetController;


  Widget build(BuildContext context) {



    return Scaffold(
      key: _scaffoldKey,
      body: SafeArea(
        bottom: false,
          child: Container(
            child: UnityWidget(
              onUnityCreated: onUnityCreated,
            ),

          ),
      ),
    );



  }

  // Callback that connects the created controller to the unity controller
  void onUnityCreated(controller) {
    _unityWidgetController = controller;
    _unityWidgetController.postMessage('Game', 'setId', 'yoojinjangjang',);

  }


}


//
// class SecondPage extends StatelessWidget {
//
//
//   // @override
//   // Widget build(BuildContext context) {
//   //
//   //
//   //   return MaterialApp(
//   //       home: UnityDemoScreen()
//   //   );
//   // }
//
//   @override
//   Widget build(BuildContext context) {
//
//
//     return MaterialApp(
//         home: UnityDemoScreen()
//            );
//     // return  Scaffold(
//     //   appBar: AppBar(
//     //     title: Text("Main Page"),
//     //   ),
//     //   body: Center(
//     //     child: ElevatedButton(
//     //       child: Text("Play Game"),
//     //       onPressed: () {
//     //         Navigator.push(
//     //             context, MaterialPageRoute(builder: (context)=>SecondPage())); },
//     //     ),
//     //   ),
//     // );
//   }
//
//
//
//
// }

// class SecondRoute extends StatelessWidget {
//   @override
//   Widget build(BuildContext context) {
//     SystemChrome.setPreferredOrientations([DeviceOrientation.landscapeRight]);
//     return Scaffold(
//       appBar: AppBar(
//         title: Text("Second Route"),
//       ),
//       body: Center(
//         child: RaisedButton(
//           onPressed: () {
//             Navigator.push(
//                 context, MaterialPageRoute(builder: (context)=>UnityDemoScreen()));
//           },
//           child: Text('Go back!'),
//         ),
//       ),
//     );
//   }
// }