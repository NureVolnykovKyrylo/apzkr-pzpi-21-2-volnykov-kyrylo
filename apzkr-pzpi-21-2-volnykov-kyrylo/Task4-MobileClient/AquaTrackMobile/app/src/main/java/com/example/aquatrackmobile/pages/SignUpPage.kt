package com.example.aquatrackmobile.pages

import androidx.compose.foundation.layout.*
import androidx.compose.material3.*
import androidx.compose.runtime.*
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.text.input.PasswordVisualTransformation
import androidx.compose.ui.unit.dp
import com.example.aquatrackmobile.viewmodels.UserViewModel
import kotlinx.coroutines.flow.collectLatest

@Composable
fun SignUpPage(viewModel: UserViewModel, onSignUpSuccess: () -> Unit, onNavigateToLogin: () -> Unit) {
    var name by remember { mutableStateOf("") }
    var email by remember { mutableStateOf("") }
    var password by remember { mutableStateOf("") }
    var confirmPassword by remember { mutableStateOf("") }

    val user by viewModel.user.collectAsState()
    val isLoading by viewModel.isLoading.collectAsState()

    var passwordsMatch by remember { mutableStateOf(true) }

    LaunchedEffect(viewModel) {
        viewModel.error.collectLatest { errorMsg ->
            // You can use a SnackbarHostState to show errors
            // snackbarHostState.showSnackbar(errorMsg)
        }
    }

    LaunchedEffect(user) {
        if (user != null) {
            onSignUpSuccess()
        }
    }

    Column(
        modifier = Modifier
            .fillMaxSize()
            .padding(16.dp),
        horizontalAlignment = Alignment.CenterHorizontally,
        verticalArrangement = Arrangement.Center
    ) {
        Text(
            text = "Create AquaTrack Account",
            style = MaterialTheme.typography.headlineMedium,
            modifier = Modifier.padding(vertical = 20.dp)
        )

        OutlinedTextField(
            value = name,
            onValueChange = { name = it },
            label = { Text("Full Name") },
            modifier = Modifier.fillMaxWidth()
        )

        Spacer(modifier = Modifier.height(8.dp))

        OutlinedTextField(
            value = email,
            onValueChange = { email = it },
            label = { Text("Email") },
            modifier = Modifier.fillMaxWidth()
        )

        Spacer(modifier = Modifier.height(8.dp))

        OutlinedTextField(
            value = password,
            onValueChange = {
                password = it
                passwordsMatch = it == confirmPassword
            },
            label = { Text("Password") },
            visualTransformation = PasswordVisualTransformation(),
            modifier = Modifier.fillMaxWidth()
        )

        Spacer(modifier = Modifier.height(8.dp))

        OutlinedTextField(
            value = confirmPassword,
            onValueChange = {
                confirmPassword = it
                passwordsMatch = it == password
            },
            label = { Text("Confirm Password") },
            visualTransformation = PasswordVisualTransformation(),
            modifier = Modifier.fillMaxWidth(),
            isError = !passwordsMatch
        )

        if (!passwordsMatch) {
            Text("Passwords do not match", color = Color.Red)
        }

        Spacer(modifier = Modifier.height(16.dp))

        Button(
            onClick = { viewModel.registerUser(email, password, name) },
            enabled = email.isNotBlank() && password.isNotBlank() && name.isNotBlank() && passwordsMatch && !isLoading,
            modifier = Modifier.fillMaxWidth()
        ) {
            Text(if (isLoading) "Signing Up..." else "Sign Up")
        }

        Spacer(modifier = Modifier.height(8.dp))

        TextButton(onClick = onNavigateToLogin) {
            Text("Already have an account? Login")
        }

        // You can also add a section to display errors inline
        // if you prefer this over a Snackbar
        /*
        viewModel.error.collectAsState(initial = null).value?.let { errorMsg ->
            Text(text = errorMsg, color = Color.Red)
        }
        */
    }
}
